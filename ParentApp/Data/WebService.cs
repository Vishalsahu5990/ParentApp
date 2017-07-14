using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ParentApp
{
	public static class WebService
	{
		public static readonly string BaseUrl = "http://m3aak.net/webservices/";
		public static String ImageUrl = "http://m3aak.net/resources/dashboard/uploads/student/";
		public static String SchoolImageUrl = "http://m3aak.net/resources/dashboard/uploads/school/";

		public static List<StudentModel> list = null;
		public static List<AttendanceModel> Presentlist = null;
		public static List<AttendanceModel> Holidaylist = null;

		public static UserModel Login(string username, string password)
		{
			UserModel um = new UserModel();
			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "weblogin?user_email=" + username + "&user_pass=" + password + "&device_token=" + StaticDataModel.DeviceToken + "&device_id=" + StaticDataModel.DeviceId));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						var json = JsonValue.Parse(Parsing);
						um.result = json["result"].ToString();
						um.responseMessage = json["responseMessage"].ToString();
						um.result = um.result.Replace("\"", string.Empty).Trim();

						if (um.result == "success")
						{
							um.user_id = json["user_id"].ToString();
							um.sms_evening_before = json["sms_evening_before"].ToString();
							um.result = json["result"].ToString();
							um.school_logo = json["school_logo"].ToString();
							um.max_speed = json["max_speed"].ToString();
							um.evening_before = json["evening_before"].ToString();
							um.school_name = json["school_name"].ToString();
							um.school_admin_name = json["school_admin_name"].ToString();
							um.user_email = json["user_email"].ToString();
							um.lang = json["lang"].ToString();
							um.school_id = json["school_id"].ToString();
							um.responseMessage = json["responseMessage"].ToString();
							um.first_name = json["first_name"].ToString();
							um.noti_on = json["noti_on"].ToString();
							um.checked_out_on = json["checked_out_on"].ToString();
							um.speed_on = json["speed_on"].ToString();
							um.morning_before = json["morning_before"].ToString();
							um.family_name = json["family_name"].ToString();
							um.role = json["role"].ToString();
							um.instant_message = json["instant_message"].ToString();
							um.school_admin_number = json["school_admin_number"].ToString();
							um.sms_checked_out_on = json["sms_checked_out_on"].ToString();
							um.school_admin = json["school_admin"].ToString();
							um.sms_speed_on = json["sms_speed_on"].ToString();
							um.sms_morning_before = json["sms_morning_before"].ToString();
							um.chat_on = json["chat_on"].ToString();
							um.wrong_route_on = json["wrong_route_on"].ToString();
							um.middle_name = json["middle_name"].ToString();
							um.sms_wrong_route_on = json["sms_wrong_route_on"].ToString();
							um.device_id = json["device_id"].ToString();
							um.sms_instant_message = json["sms_instant_message"].ToString();
							um.contact_number = json["contact_number"].ToString();
							um.user_name = json["user_name"].ToString();
							um.mobile_number = json["mobile_number"].ToString();
							um.sms_max_speed = json["sms_max_speed"].ToString();
							um.sms_checked_in_on = json["sms_checked_in_on"].ToString();
							um.checked_in_on = json["checked_in_on"].ToString();
						}
					}
				}
				return um;
			}
			catch (Exception ex)
			{
				return um;
			}
		}
	public static string GetContactNo()
		{
			string contact_no = string.Empty;
			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "getSiteNumber"));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						var json = JsonValue.Parse(Parsing);
						contact_no = json["mobile_number"].ToString();

					}
				}
				return contact_no;
			}
			catch (Exception ex)
			{
				return contact_no;
			}
		}
		public static string ParentLogout()
		{
			string ResponseCode = string.Empty;
			try
			{
				WebClient wc = new WebClient();
				wc.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
				wc.Headers.Add(HttpRequestHeader.Accept, "application/json, text/javascript, */*; q=0.01");
				wc.Headers.Add("X-Requested-With", "XMLHttpRequest");
				JObject j = new JObject();
				j.Add("parent_id", StaticDataModel.UserId.ToString());

				byte[] dataBytes = Encoding.UTF8.GetBytes(j.ToString());
				byte[] responseBytes = wc.UploadData(new Uri(BaseUrl + "parentLogout"), "POST", dataBytes);
				string responseString = Encoding.UTF8.GetString(responseBytes);
				var json = JsonValue.Parse(responseString);
				Console.WriteLine(responseString);
				ResponseCode = Convert.ToString(json["result"].ToString());

				return ResponseCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return ResponseCode;
			}
				
		}
		public static List<StudentModel> GetStudentByParentId()
		{
			 list = null;
			 list = new List<StudentModel>();
			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "getStudentByParent?s_parent_id=" + StaticDataModel.UserId ));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						var json = JsonValue.Parse(Parsing);
						JObject jObj = JObject.Parse(Parsing);
						var DataValues = jObj["child"];
					    
						foreach (var item in DataValues)
						{
							list.Add(new StudentModel
							{
								grand_name = Convert.ToString(item["grand_name"].ToString()),
								s_email = Convert.ToString(item["s_email"].ToString()),
								p_status_id = Convert.ToString(item["p_status_id"].ToString()),
								s_contact = Convert.ToString(item["s_contact"].ToString()),
								s_lname = Convert.ToString(item["s_lname"].ToString()),
								school_name = Convert.ToString(item["school_name"].ToString()),
								s_route_id = Convert.ToString(item["s_route_id"].ToString()),
								student_id = Convert.ToString(item["student_id"].ToString()),
								student_lat = Convert.ToString(item["student_lat"].ToString()),
								blood_type = Convert.ToString(item["blood_type"].ToString()),
								father_name = Convert.ToString(item["father_name"].ToString()),
								blink_status = Convert.ToString(item["blink_status"].ToString()),
								s_address = Convert.ToString(item["s_address"].ToString()),
								nationality = Convert.ToString(item["nationality"].ToString()),
								s_school_id = Convert.ToString(item["s_school_id"].ToString()),
								gender = Convert.ToString(item["gender"].ToString()),
								s_fname = Convert.ToString(item["s_fname"].ToString()),
								family_name = Convert.ToString(item["family_name"].ToString()),
								student_lng = Convert.ToString(item["student_lng"].ToString()),
								student_class = Convert.ToString(item["student_class"].ToString()),
								s_image_path = Convert.ToString(item["s_image_path"].ToString()),
								driver_name = Convert.ToString(item["s_zip"].ToString()),
								driver_contact = Convert.ToString(item["s_pass"].ToString()),
								dob = Convert.ToString(item["dob"].ToString()),

							});

					}

					}
				}
				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return list;

			}
		}
		public static StudentLocationModel GetLatLongForStudentTracking(int student_id)
		{
			StudentLocationModel list = new StudentLocationModel();
			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "student_lat_lng?student_id=" + student_id));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						var json = JsonValue.Parse(Parsing);
						JObject item = JObject.Parse(Parsing);

						list.student_lat = Convert.ToDouble(item["student_lat"].ToString());
						list.student_lng = Convert.ToDouble(item["student_lng"].ToString());
						list.source_lat = Convert.ToDouble(item["source_lat"].ToString());
						list.source_lng = Convert.ToDouble(item["source_lng"].ToString());
						list.destination_lat = Convert.ToDouble(item["destination_lat"].ToString());
						list.destination_lng = Convert.ToDouble(item["destination_lng"].ToString());
							

					}
				}
				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return list;

			}
		}
		public static BusTrackingModel GetRealtimeBusLatLong(int student_id,int s_route_id)
		{
			BusTrackingModel list = new BusTrackingModel();
			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "get_realtime_location?student_id=" + student_id+"&route_id="+s_route_id));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						JObject jObj = JObject.Parse(content);
						var item = jObj["route_lat_lng"];
						//var json = JsonValue.Parse(Parsing);
						//var item = json["route_lat_lng"];

						list.track_date = Convert.ToString(item[0]["track_date"].ToString());
						list.route_id = Convert.ToInt32(item[0]["route_id"].ToString());
						list.driver_id = Convert.ToInt32(item[0]["driver_id"].ToString());
						list.lng = Convert.ToDouble(item[0]["lng"].ToString());
						list.track_id = Convert.ToInt32(item[0]["track_id"].ToString());
						list.lat = Convert.ToDouble(item[0]["lat"].ToString());

					}
				}
				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return list;

			}
		}
		public static int ForgotPassword(string email)
		{
			int ResponseCode = 0;

			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "forgot_password?user_email=" + email ));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						JObject jObj = JObject.Parse(content);
						var item = jObj["result"].ToString();
						if (item.Equals("success"))
						{
							ResponseCode = 200;
						}
						else
						{ 
						
						}

					}
				}
				return ResponseCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return ResponseCode;

			}
		}
		public static int ChangePassword(string username,string current_password,string new_password)
		{
			int ResponseCode = 0;

			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "changePassword?user_email=" + username+"&user_pass="+current_password+"&first_name="+new_password));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						JObject jObj = JObject.Parse(content);
						var item = jObj["result"].ToString();
						if (item.Equals("success"))
						{
							ResponseCode = 200;
						}
						else
						{

						}
					}
				}
				return ResponseCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return ResponseCode;

			}


		}
		public static int SaveSettings(SettingsModel sm)
		{
			int ResponseCode = 0;

			try
			{
				var request = HttpWebRequest.Create(string.Format(
					BaseUrl + "save_parent_setting?" +
					"lang=" + sm.language + "&noti_on=" 
					+ sm.sound_noti + "&chat_on=" + sm.sound_chat +
					"&checked_in_on=" + sm.checkedin_noti
					+ "&checked_out_on=" + sm.checkedout_noti + "&speed_on=" 
					+ sm.speed_noti + "&max_speed=" + sm.speed + "&wrong_route_on=" 
					+ sm.wrongroute_noti + "&user_id=" + StaticDataModel.UserId
					+ "&sms_checked_in_on=" + sm.checkedin_notisms + "&sms_checked_out_on="
					+ sm.checkedout_notisms + "&sms_speed_on=" + sm.speed_notisms + "&sms_max_speed="
					+ sm.speed_sms + "&sms_wrong_route_on=" + sm.wrongroute_notisms +
					 "&instant_message=" + sm.driver_noti + "&morning_before=" +
					 sm.morning_noti + "&evening_before=" + sm.evening_noti + 
					"&sms_instant_message=" + sm.driver_notisms + "&sms_morning_before=" 
					+ sm.morning_notisms + "&sms_evening_before=" 
					+ sm.evening_notisms
					+"&sound_setting="+sm.sound_noti+
					"&chat_sound="+sm.sound_chat));
				
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						JObject jObj = JObject.Parse(content);
						var item = jObj["result"].ToString();
						if (item.Equals("success"))
						{
							ResponseCode = 200;
						}
						else
						{

						}
					}
				}
				return ResponseCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return ResponseCode;

			}


		}
		public static List< NotificationModel> GetAllNotification()
		{
			int ResponseCode = 0;
			List<NotificationModel> list =new   List< NotificationModel>();
			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "notification?method=all_notification&route_id=" + StaticDataModel.StudentRouteId + "&parent_id=" + StaticDataModel.UserId ));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						JObject jObj = JObject.Parse(Parsing);
						var DataValues = jObj["notifications"];

						foreach (var item in DataValues)
						{
							list.Add(new NotificationModel
							{
											
								noti_id = Convert.ToString(item["noti_id"].ToString()),
								route_id = Convert.ToString(item["route_id"].ToString()),
								student_id = Convert.ToString(item["student_id"].ToString()),
								noti_type = Convert.ToString(item["noti_type"].ToString()),
								date = Convert.ToString(item["date"].ToString()),
								msg = Convert.ToString(item["msg"].ToString()),
								parent_id = Convert.ToString(item["parent_id"].ToString()),
							});

						}
					}
				}
				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return list;

			}


		}
		public static List<ChatModel> GetAllConversation()
		{
			int ResponseCode = 0;
			List<ChatModel> list = new List<ChatModel>();
			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "chatting?reciever_id=" + StaticDataModel.UserId));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						JObject jObj = JObject.Parse(Parsing);
						var DataValues = jObj["details"];

						foreach (var item in DataValues)
						{
							list.Add(new ChatModel
							{

								sender = Convert.ToInt32(item["sender"].ToString()),
								time = Convert.ToString(item["time"].ToString()),
								status = Convert.ToInt32(item["status"].ToString()),
								sender_id = Convert.ToInt32(item["sender_id"].ToString()),
								reciever_id = Convert.ToInt32(item["reciever_id"].ToString()),
								msg = Convert.ToString(item["msg"].ToString())

							});

						}
					}
				}
				return list;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return list;

			}


		}
		public static int SendMessage(string adminid,string msg)
		{
			int ResponseCode = 0;

			try
			{ 
				var url="send_message?sender_id=" + StaticDataModel.UserId + "&reciever_id=" + adminid + "&msg=" + msg;
				url.Replace(" ", "%20");
				var request = HttpWebRequest.Create(string.Format(BaseUrl + url));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						JObject jObj = JObject.Parse(content);
						var item = jObj["result"].ToString();
						if (item.Equals("success"))
						{
							ResponseCode = 200;
						}
						else
						{

						}
					}
				}
				return ResponseCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return ResponseCode;

			}
		}
		public static int SubmitAbsentReport(int student_id,string dates,string reason)
		{
			int ResponseCode = 0;
			try
			{
				var json1 = "{ 'student_id':'"+student_id+"','absent_date':'"+dates+"','reason':'"+reason+"'}";
				WebClient wc = new WebClient();
				wc.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
				wc.Headers.Add(HttpRequestHeader.Accept, "application/json, text/javascript, */*; q=0.01");
				wc.Headers.Add("X-Requested-With", "XMLHttpRequest");
				///var json1 = JsonConvert.SerializeObject(j);

				byte[] dataBytes = Encoding.UTF8.GetBytes(json1);
				byte[] responseBytes = wc.UploadData(new Uri(BaseUrl + "add_student_absent"), "POST", dataBytes);
				string responseString = Encoding.UTF8.GetString(responseBytes);
				var json = JObject.Parse(responseString);
				Console.WriteLine(responseString);
				var msg = Convert.ToString(json["result"].ToString());
				if (msg == "success")
					ResponseCode = 200;

				return ResponseCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return ResponseCode;
			}   

		}
		public static List<AttendanceModel> GetStudentAttendance(int student_id)
		{
			Presentlist = new List<AttendanceModel>();
			Holidaylist = new List<AttendanceModel>();
			List<ListModel> ArrayList = new List<ListModel>();

			try
			{
				var request = HttpWebRequest.Create(string.Format(BaseUrl + "student_attendance?student_id=" + student_id));
				request.ContentType = "application/json";
				request.Method = "GET";
				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					if (response.StatusCode != HttpStatusCode.OK)
						Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = reader.ReadToEnd();
						string Parsing = content.ToString();
						var json = JsonValue.Parse(Parsing);
						JObject jObj = JObject.Parse(Parsing);
						var DataValues = jObj["holiday"];
						var DataValues1 = jObj["present"];
						foreach (var item in DataValues)
						{ 
							Holidaylist.Add(new AttendanceModel
						{
								
								holiday_date = Convert.ToDateTime(item["holiday_date"].ToString()),
								h_id = Convert.ToInt32(item["h_id"].ToString()),
							holiday_name = Convert.ToString(item["holiday_name"].ToString()),
							school_id = Convert.ToInt32(item["school_id"].ToString()),
							});
						}
						foreach (var item in DataValues1)
						{
							Presentlist.Add(new AttendanceModel
							{

								date = Convert.ToDateTime(item["date"].ToString()),
								h_id = Convert.ToInt32(item["a_id"].ToString()),

								});
						}


					}
				}
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;

			}
		}
		public static string GetStudentPresentAbsent(int student_id,int school_id, string start_date, string end_date)
		{
			string Response = string.Empty;
			try
			{
				//var json1 = "{ 'start_date':'" + start_date + "','end_date':'" + end_date + "','student_id':'" + student_id + "','school_id':'"+school_id+"'}";
				var json1 = "{'start_date':'"+start_date+"','end_date':'"+end_date+"','school_id':'"+school_id+"','student_id':'"+school_id+"'}";
				WebClient wc = new WebClient();
				wc.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
				wc.Headers.Add(HttpRequestHeader.Accept, "application/json, text/javascript, */*; q=0.01");
				wc.Headers.Add("X-Requested-With", "XMLHttpRequest");
				///var json1 = JsonConvert.SerializeObject(j);

				byte[] dataBytes = Encoding.UTF8.GetBytes(json1);
				byte[] responseBytes = wc.UploadData(new Uri(BaseUrl + "get_student_present_absent"), "POST", dataBytes);
				string responseString = Encoding.UTF8.GetString(responseBytes);
				var json = JObject.Parse(responseString);
				Console.WriteLine(responseString);
				var msg = Convert.ToString(json["result"].ToString());
				if (msg == "success")
				{ 
				 Response = Convert.ToString(json["present_day"].ToString())+","+
					               Convert.ToString(json["absent_day"].ToString()) + "," +
					               Convert.ToString(json["holiday_days"].ToString());
				}

				return Response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Response;
			}

		}
	}
}
