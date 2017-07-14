using System;
using System.Collections.Generic;
using UIKit;
namespace ParentApp
{
	public static class GetPickerUi
	{
		public static async void SetupPicker(UITextField textfield, List<StudentModel> item)
		{
			try
			{
				// Setup the picker and model
				GenderPickerViewModel model = new GenderPickerViewModel(item);

				UIPickerView agepicker = new UIPickerView();
				agepicker.ShowSelectionIndicator = true;
				agepicker.Model = model;
				agepicker.BackgroundColor = UIColor.White;

				// Setup the toolbar
				UIToolbar toolbar = new UIToolbar();
				toolbar.BarStyle = UIBarStyle.BlackOpaque;
				toolbar.BackgroundColor = new UIColor(red: 0f, green: 0.687f, blue: 0.495f, alpha: 1.0f);
				toolbar.Translucent = true;
				toolbar.SizeToFit();

				// Create a 'done' button for the toolbar and add it to the toolbar
				UIBarButtonItem doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done,
																 (s, e) =>
																 {

																	 try
																	 {
						                                               if (model != null)
																		 {
																			 var data = model.SelectedItem.Split(',');
																			 textfield.Text = data[0];
																			 ReportAbsent.SelectedStudentId = Convert.ToInt32(data[1]);
																		 }
																	 }
																	 catch (Exception ex)
																	 {

																	 }
																	 textfield.ResignFirstResponder();
																 });
				doneButton.TintColor = UIColor.White;
				toolbar.SetItems(new UIBarButtonItem[] { doneButton }, true);

				// Tell the textbox to use the picker for input
				textfield.InputView = agepicker;

				// Display the toolbar over the pickers
				textfield.InputAccessoryView = toolbar;


			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
		public class GenderPickerViewModel : UIPickerViewModel
		{
			List<StudentModel> items;
			public event EventHandler<EventArgs> ValueChanged;
			protected int selectedIndex = 0;
			public GenderPickerViewModel()
			{

			}
			public GenderPickerViewModel(List<StudentModel> Items)
			{
				items = Items;
			}
			public string SelectedItem
			{

				get
				{
					if (items != null)
					{
						if (items.Count > 0)
							return items[selectedIndex].s_fname.ToString() + " " + items[selectedIndex].family_name.ToString()+","+items[selectedIndex].student_id.ToString();
						else
							return "";
					}
					else
					{
						return "";
					}
				}
			}
			public override nint GetComponentCount(UIPickerView picker)
			{
				return 1;
			}

			public override nint GetRowsInComponent(UIPickerView picker, nint component)
			{
				if (items != null)
					return items.Count;
				else
					return 0;
			}

			public override string GetTitle(UIPickerView picker, nint row, nint component)
			{
				//Console.WriteLine(items[(int)row].Name.ToString());
				//Console.WriteLine(items[(int)row].ToString());
				//return items[(int)row].ToString();
				return items[(int)row].s_fname.ToString() + " " + items[(int)row].family_name.ToString();

			}
			public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
			{
				return 40f;
			}
			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				selectedIndex = (int)row;

				//Console.WriteLine(tll[(int)row].name.ToString());
				if (this.ValueChanged != null)
				{
					this.ValueChanged(this, new EventArgs());
				}
			}
		}
	}
}
