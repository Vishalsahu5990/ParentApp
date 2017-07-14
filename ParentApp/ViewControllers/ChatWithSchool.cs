using Foundation;
using System;
using UIKit;
using SDWebImage;
using Quobject.SocketIoClientDotNet.Client;
using Square.SocketRocket;
using System.Collections.Generic;
using BigTed;
using System.Threading.Tasks;
using SDWebImage;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Drawing;

namespace ParentApp
{
    public partial class ChatWithSchool : UIViewController
    {
		public string SocketUrl = "ws://m3aak.net/chat?name=";
		private Socket socket;
		List<ChatModel> model = null;
		string fullname = string.Empty;
		string adminid = string.Empty;
		public bool IsConnected = false;
		WebSocket webSocket;
		UITextField textfield;
		CoreGraphics.CGRect tableviewActualSize;
		AppDelegate ap = null;
		CoreGraphics.CGRect typingimage_frame;
public static event EventHandler NotificatonCountChanged_Chat = delegate { };
        public ChatWithSchool (IntPtr handle) : base (handle)
        {
        
		}



		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

NSUrl url = new NSUrl(SocketUrl + StaticDataModel.UserId);
webSocket = new WebSocket(url);
webSocket.Open();
             ap = new AppDelegate();
			tableviewActualSize=	tblConversation.Frame;
			textfield = new UITextField(new RectangleF((float)txtWritehere.Frame.X, 30, (float)txtWritehere.Frame.Width, (float)txtWritehere.Frame.Height));
			textfield.BackgroundColor = UIColor.White;
			textfield.Layer.CornerRadius = 14;
			textfield.Layer.BorderWidth = 4;
			textfield.Layer.BorderColor = new UIColor(red: 1f, green: 0.79f, blue: 0f, alpha: 1.0f).CGColor;
			textfield.AttributedPlaceholder = new NSAttributedString("Write here", null, UIColor.DarkGray);
			textfield.Font = txtWritehere.Font;
			textfield.BorderStyle = UITextBorderStyle.RoundedRect;
			StaticMethods.SetPadding(textfield, 5);

			textfield.ShouldReturn += (textField) =>
	     {

		    textField.ResignFirstResponder();
			
		return true;
	};

			txtWritehere.ShouldReturn += (textField) =>
			{

				textField.ResignFirstResponder();
				return true;
			};

			UIKeyboard.Notifications.ObserveWillShow((sender, e) =>
			{
				textfield.BecomeFirstResponder();
				if (StaticMethods.DeviceType() == "ipad")
				{
					imgTyping.Frame = new CoreGraphics.CGRect(typingimage_frame.X,
															  typingimage_frame.Y - 300,
															  typingimage_frame.Width,
															  typingimage_frame.Height);
				}
				else
				{ 
				imgTyping.Frame = new CoreGraphics.CGRect(typingimage_frame.X,
															  typingimage_frame.Y - 200,
															  typingimage_frame.Width,
															  typingimage_frame.Height);
				}


			});
			UIKeyboard.Notifications.ObserveWillHide((sender, e) =>
			{
				textfield.BecomeFirstResponder();
				 tblConversation.Frame = tableviewActualSize;
				txtWritehere.Text = textfield.Text;
				imgTyping.Frame = typingimage_frame;
			});

			txtWritehere.AttributedPlaceholder = new NSAttributedString("Write here", null, UIColor.DarkGray);

			this.txtWritehere.ShouldChangeCharacters = (textField, range, replacementString) =>
			{

				SendSocketMessage("type");
				return true;
			};
			GetStudentConversation();
			// Create request for remote resource

			webSocket.WebSocketOpened += (sender, e) =>
			{
				// the socket was opened, so we can start using it
				IsConnected = true;
				SendSocketMessage("read");

			};


            webSocket.ReceivedMessage += (sender, e) =>
			{
				JObject jObj = JObject.Parse(e.Message.ToString());
				Console.WriteLine(e.Message.ToString());
				StaticDataModel.SocketSessionId = jObj["sessionId"].ToString();
				StaticDataModel.SocketFlag = jObj["flag"].ToString();
				if (StaticDataModel.SocketFlag == "message" )
														{
					
					var id = jObj["user_id"].ToString();
					var message = jObj["message"].ToString();
					//if (message != "1" && message != "read_unread_check")
					//{
						if (id == StaticDataModel.UserId.ToString())
						{
						  if (message == "1")
							{
							    imgTyping.Hidden = false;
								Console.WriteLine("Typing....");
							}
						  else if (message == "read_unread_check")
							{
							Console.WriteLine("Seen....");
							model.Select(c => { c.status = 1; return c; }).ToList();
							RefreshTableview();
							imgTyping.Hidden = true;
							}
						  else
							{
						    addCurrentMessagesTotableview(0, 0, message);
							SendSocketMessage("read");
							imgTyping.Hidden = true;
						    }

						}
					//}



				}

			
		    };
			PrepareUI();
			SetData();


			imgBack.AddGestureRecognizer(new UITapGestureRecognizer(ro =>
		   {
				
			   if (!StaticDataModel.isFromNotification)
			   {
				   this.DismissModalViewController(false);
			   }
			   else
			   { 
				if (StaticDataModel.CurrentLanguage == "en")
			{
					  
						Home home = ap.MainStoryboard.InstantiateViewController("Home") as Home;
UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("en")
			}
			else
			{
				Home home = ap.Main_ArabicStoryboard.InstantiateViewController("Home") as Home;
UIApplication.SharedApplication.KeyWindow.RootViewController = home;
				//StaticMethods.ChangeLocalization("ar");
			}
				}

		   }));

			txtWritehere.EditingDidBegin+= TxtWritehere_EditingDidBegin;

		}
		public override void ViewWillAppear(bool animated)
		{
			StaticDataModel.NotificationBadgeCount_Chat = 0;
			//webSocket.Open();

			base.ViewWillAppear(animated);

			AnimatedImageView.GetAnimatedImageView("http://m3aak.net/resources/front/Images/iphone.gif", imgTyping);
			imgTyping.Hidden = true;
		}
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			StaticDataModel.NotificationBadgeCount_Chat = 0;
		}
		public override void ViewDidDisappear(bool animated) 
		{
			base.ViewDidDisappear(animated);
			NotificatonCountChanged_Chat(this, null);
			StaticDataModel.isFromNotification = false;
		}

		void TxtWritehere_EditingDidBegin(object sender, EventArgs e)
		{
			tblConversation.Frame = new RectangleF((float)tblConversation.Frame.X,(float)tblConversation.Frame.Y,(float)tblConversation.Frame.Width,(float)StaticDataModel.DeviceHeight/2-150);
			UIView dismiss = new UIView(uiviewTextwithButton.Frame);
			dismiss.BackgroundColor = new UIColor(red: 0.999f, green: 1f, blue: 1f, alpha: 1.0f);



			UIButton buttonSend = new UIButton(new RectangleF((float)btnSend.Frame.X, 30, (float)btnSend.Frame.Width, (float)btnSend.Frame.Height));
			buttonSend.BackgroundColor = new UIColor(red: 0.217f, green: 0.253f, blue: 0.408f, alpha: 1.0f);
			buttonSend.SetTitle("SEND", UIControlState.Normal);
			buttonSend.Font = btnSend.Font;
			buttonSend.Layer.CornerRadius = 14;

			buttonSend.TouchUpInside += (senders, es) => 
			 {
				InvokeOnMainThread(() => 
				{
					txtWritehere.ResignFirstResponder();    
					SendProcess(); 
				});

			 };

			dismiss.AddSubview(textfield);
			dismiss.AddSubview(buttonSend);
			txtWritehere.InputAccessoryView = dismiss;
		}

        

		partial void BtnSend_TouchUpInside(UIButton sender)
		{
			SendProcess();	

		}
		public void SendProcess()
		{ 
			try
			{
				if (!txtWritehere.Text.Equals(string.Empty) || !textfield.Text.Equals(string.Empty))
			{
				//if (IsConnected)
					if (true)
				{
					if (textfield.Text != string.Empty)
					{
						var msg = textfield.Text;
					}
					else
					{ 
					var msg = txtWritehere.Text;
					}
						
					JObject j = new JObject();
j.Add("sessionId", StaticDataModel.SocketSessionId);
					j.Add("message",  textfield.Text);
					j.Add("flag", "message");
					j.Add("chat_type", "1");
					j.Add("user_id", adminid);
					j.Add("sender_name", fullname);
					webSocket.Send((NSString)j.ToString());
					addCurrentMessagesTotableview(1, 0, textfield.Text);
                    txtWritehere.Text = string.Empty;
					textfield.Text = string.Empty;
				}


			}
			else
			{
				BTProgressHUD.ShowToast("Please write message and try again!.", false, 10000);

			}

			}
			catch (Exception ex)
			{

			}
		}
		private void SendSocketMessage(string msg)
		{ 
		try
			{
				JObject j = new JObject();
				j.Add("sessionId", StaticDataModel.SocketSessionId);
				j.Add("message", msg);
				j.Add("flag", "message");
				j.Add("chat_type", "1");
				j.Add("user_id", adminid);
				j.Add("sender_name", fullname);
				webSocket.Send((NSString)j.ToString());
			}
			catch (Exception ex)
			{

			}
		}
		private void addCurrentMessagesTotableview(int sender,int status,string msg)
		{ 
		try
			{
				model.Add(new ChatModel
				{

					sender = sender,
					time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"),
					status = status,
					sender_id = StaticDataModel.UserId,
					reciever_id =Convert.ToInt32( adminid),
					msg = msg

				});
				tblConversation.Source = new Chat_tableviewsource(model);
				tblConversation.RowHeight = UITableView.AutomaticDimension;
				tblConversation.EstimatedRowHeight = 85f;
				Add(tblConversation);
				tblConversation.ReloadData();
				tblConversation.ScrollToRow(Foundation.NSIndexPath.FromItemSection(model.Count - 1, 0), UITableViewScrollPosition.Top, false);
				SendMessage(adminid, msg);
			}
			catch (Exception ex)
			{

			}
		}
		private void RefreshTableview()
		{ 
		try
			{
				tblConversation.Source = new Chat_tableviewsource(model);
				tblConversation.RowHeight = UITableView.AutomaticDimension;
				tblConversation.EstimatedRowHeight = 85f;
				Add(tblConversation);
				tblConversation.ReloadData();
				tblConversation.ScrollToRow(Foundation.NSIndexPath.FromItemSection(model.Count - 1, 0), UITableViewScrollPosition.Top, false);

			}
			catch (Exception ex)
			{

			}
		}
		private void GetUserPrefrences()
			{ 
				try
			{
						Login.dictionary = NSUserDefaults.StandardUserDefaults.DictionaryForKey(Login.key);
				 adminid = Login.dictionary["school_admin"].ToString().Replace("\"", string.Empty).Trim();
				 fullname = Login.dictionary["first_name"].ToString().Replace("\"", string.Empty).Trim()
						                 +" "+Login.dictionary["middle_name"].ToString().Replace("\"", string.Empty).Trim()+
						                " "+ Login.dictionary["family_name"].ToString().Replace("\"", string.Empty).Trim();
				
			}
			catch (Exception ex)
			{

			}
		}

		private void PrepareUI()
		{
			try
			{

				txtWritehere.Layer.BorderWidth = 4;
				txtWritehere.Layer.BorderColor=new UIColor(red: 1f, green: 0.79f, blue: 0f, alpha: 1.0f).CGColor;
			    StaticMethods.SetPadding(txtWritehere, 10);
				typingimage_frame = imgTyping.Frame;
			}
			catch (Exception ex)
			{

			}
		}
		private void SetData()
		{
			try
			{
				GetUserPrefrences();
				if (StaticDataModel.ProfilePic != string.Empty)
				{
					lblSchoolName.SetTitle(StaticDataModel.SchoolName, UIControlState.Normal);
					StaticDataModel.ProfilePic = StaticDataModel.ProfilePic.Replace("\"", string.Empty).Trim();
					StaticDataModel.ProfilePic = StaticDataModel.ProfilePic.Replace(" ", "%20").Trim();

					var url = WebService.SchoolImageUrl + StaticDataModel.ProfilePic;
					Console.WriteLine(url);
					imgSchoolLogo.SetImage(
						url: new NSUrl(url),
											placeholder: UIImage.FromBundle("dummmyprofile_withBorder.png")
										  );
				}
			}
			catch (Exception ex)
			{

			}
		}
		private void GetStudentConversation()
		{
			model = new List<ChatModel>();
			try
			{
				BTProgressHUD.Show();
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

						 model = WebService.GetAllConversation();
					}
				///
				).ContinueWith(
					t =>
					{

						if (model != null)
						{
							tblConversation.Source = new Chat_tableviewsource(model);
							tblConversation.RowHeight = UITableView.AutomaticDimension;
							tblConversation.EstimatedRowHeight = 85f;
							Add(tblConversation);
						    tblConversation.ReloadData();
							if (model.Count > 0)
							{ 
						tblConversation.ScrollToRow(Foundation.NSIndexPath.FromItemSection(model.Count - 1, 0), UITableViewScrollPosition.Top, false);
							
						   }
	
								
						}
						else
						{

						}
						BTProgressHUD.Dismiss();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
			catch (Exception ex)
			{

			}
			finally
			{

			}
		}
		private void SendMessage(string adminid,string msg)
		{
			int responseCode = 0;
			try
			{
				
				Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{

					responseCode = WebService.SendMessage(adminid,msg);
					}
				///
				).ContinueWith(
					t =>
					{ 

						if (responseCode == 200)
						{
							

						}
						else
						{

						}
						

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
			}
			catch (Exception ex)
			{

			}
			finally
			{

			}
		}
		class SocketDelegate : WebSocketDelegate
		{
			public override void WebSocketOpened(WebSocket webSocket)
			{
				// the socket was opened
			}
			public override void WebSocketClosed(WebSocket webSocket, StatusCode code, string reason, bool wasClean)
			{
				// the connection was closed
			}
			public override void WebSocketFailed(WebSocket webSocket, NSError error)
			{
				// there was an error
			}
			public override void ReceivedMessage(WebSocket webSocket, NSObject message)
			{
				// we received a message
			}
			public override void ReceivedPong(WebSocket webSocket, NSData pongPayload)
			{
				// respond to a ping
			}
		}
    }
}