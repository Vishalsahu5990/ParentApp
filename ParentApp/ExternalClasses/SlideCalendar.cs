using System;
using UIKit;
using Foundation;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using System.Linq;
using System.Globalization;
namespace ParentApp
{
	public class SlideCalendar : UIView
	{
		const int X_BORDER_OFFSET = 0; //the offset for the calendar border
		const int Y_BORDER_OFFSET = 15; //the offset from the line to the name of days
		const int DAYS_IN_A_WEEK = 7;
		const int MAX_WEEKS_TO_DISPLAY = 6; //the rows
		const int MONTH_NAME_HEIGTH = 80;
		const int DAY_NAME_HEIGTH = 23;
		const int SLIDER_HEIGTH = 30;
		const int ARROW_BTN_WIDTH = 30;

		const int IPHONE_4_HEIGHT = 480;
		const int IPHONE_5_HEIGHT = 568;
		const int IPHONE_6_HEIGHT = 667;
		const int IPHONE_6P_HEIGHT = 736;

		int _singleDayFrameSize; //the size of a side of the day
		int _borderOffset; //the new offset calculated accordingly to screen size and days of the month\
		int _calendarHeigth;


		UILabel _monthNameLabel;
		UISlider _selectMonthSlider;
		UIView _monthView;
		UIButton _nextMonthBtn;
		UIButton _prevMonthBtn;

		DateTime _currentDate; //the current date set on the calendar
		DateTime _minDate; //the min date shown, the min value of the slider
		DateTime _maxDate; //the max date shown, the max value of the slider

		//by vishal
		List<int> ListofSelectedDates = new List<int>();

		public delegate void SelectedDayDelegate(DateTime selectedDate,List<int> ListofSelectedDates);
		public event SelectedDayDelegate OnDaySelected;
		//by vishal
		public delegate void NextMonthDelegate(DateTime selectedDate);
		public event NextMonthDelegate NextMonth;

		public delegate void PreviousMonthDelegate(DateTime selectedDate);
		public event PreviousMonthDelegate PreviousMonth;
		UIColor _fontColor = UIColor.Black;




		//public delegate void ArrowPressedDelegate (bool next);
		//public event ArrowPressedDelegate OnArrowPressed;

		/// <summary>
		/// Initializes a new instance of the FPCalendar class.
		/// </summary>
		/// <param name="position">The frame position in the view.</param>
		/// <param name="startDate">The month displayed when the calendar is created</param>
		/// <param name="minDate">the minimum date displayed by the calendar (the min value of the slider)</param>
		/// <param name="maxDate">the maximum date displayed by the calendar (the max value of the slider).</param>
		public SlideCalendar(CGPoint position, DateTime startDate, DateTime minDate, DateTime maxDate)
		{
			_currentDate = startDate;
			_minDate = minDate;
			_maxDate = maxDate;

			GenerateSlider(minDate, maxDate);
			CalculateSingleDaySize();

			nfloat widthOffset = 0;
			if (position.X < 0)
				widthOffset = -(position.X * 2);

			this.Frame = (new CGRect(position, new CGSize(UIScreen.MainScreen.Bounds.Width + widthOffset, GetHeight())));

			_monthView = new UIView(new CGRect(position.X, DAY_NAME_HEIGTH, UIScreen.MainScreen.Bounds.Width, _calendarHeigth-50));

			GenerateSingleMonth(_currentDate);
			UpdateMonthName(_currentDate, true);
			this.AddSubview(_monthNameLabel);
			this.AddSubview(_monthView);

			AddWeekDays();



			//this.Layer.BorderWidth = 0.5f;
			//this.Layer.BorderColor = UIColor.FromRGB(230, 230, 230).CGColor;

		}

		public SlideCalendar(CGPoint position) : this(position, DateTime.Now, new DateTime(2000, 1, 1), DateTime.Now)
		{

		}

		public nfloat GetHeight()
		{
			//return _calendarHeigth + SLIDER_HEIGTH + MONTH_NAME_HEIGTH + DAY_NAME_HEIGTH + Y_BORDER_OFFSET;
			return _calendarHeigth;
		}

		//used to display a string in the month name view, used for search results
		public void ChangeMonthString(string value)
		{
			_monthNameLabel.Text = value;
		}

		//move the calendar to a date from outside (used in the random function);
		public void GoToDate(DateTime date)
		{
			_currentDate = date;
			DayHasBeenSelected(_currentDate.Day);
			UpdateSliderValueFromDate(_currentDate);
		}

		public void DisposeCalendar()
		{
			_selectMonthSlider.ValueChanged -= OnSliderValueChanged;
			//_nextMonthBtn.TouchUpInside -= OnArrowTouchUp;
			//_prevMonthBtn.TouchUpInside -= OnArrowTouchUp;

			_monthNameLabel.RemoveFromSuperview();
			_monthNameLabel.Dispose();
			_selectMonthSlider.RemoveFromSuperview();
			_selectMonthSlider.Dispose();
			_monthView.RemoveFromSuperview();
			_monthView.Dispose();
			_nextMonthBtn.RemoveFromSuperview();
			_prevMonthBtn.Dispose();
			//by vishal
			ListofSelectedDates = null;
		}

		//according to the device type and screen size, the size of the day frame gets calculated here
		void CalculateSingleDaySize()
		{
			var availableWidth = UIScreen.MainScreen.Bounds.Width - X_BORDER_OFFSET * 2;
			_singleDayFrameSize = (int)availableWidth / DAYS_IN_A_WEEK - 7;
			_borderOffset = (int)X_BORDER_OFFSET + (((int)availableWidth % DAYS_IN_A_WEEK) / 2);

			_calendarHeigth = (int)_singleDayFrameSize * MAX_WEEKS_TO_DISPLAY + Y_BORDER_OFFSET;
			_calendarHeigth++;
		}

		//updates the Name of the month at the top
		//if selected means the date visualized is complete (day, year)
		//if not selected it displays only the month
		void UpdateMonthName(DateTime monthDate, bool selected)
		{
			if (_monthNameLabel == null)
			{
				_monthNameLabel = new UILabel(new CGRect(0, this.Frame.Height - MONTH_NAME_HEIGTH , UIScreen.MainScreen.Bounds.Width, MONTH_NAME_HEIGTH ));
				//_monthNameLabel.BackgroundColor = UIColor.Red;
				_monthNameLabel.TextColor = _fontColor;
				_monthNameLabel.TextAlignment = UITextAlignment.Center;
				_monthNameLabel.Font = UIFont.FromName("HelveticaNeue-Light", 33f);//  ("Baskerville-Bold", 30f);
				AddArrows();
			}

			if (selected)
			{
				_monthNameLabel.Text = monthDate.ToString("d");
			}
			else
				_monthNameLabel.Text = monthDate.ToString("MMMM yyyy");

			_monthNameLabel.Hidden = true;
		}

		//add left and right arrows next to the month name
		void AddArrows()
		{
			_prevMonthBtn = new UIButton(new CGRect(X_BORDER_OFFSET, _monthNameLabel.Frame.Y, ARROW_BTN_WIDTH, _monthNameLabel.Frame.Height));
			_prevMonthBtn.SetImage(UIImage.FromFile("calendar-prev-black.png"), UIControlState.Normal);
			_prevMonthBtn.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			_prevMonthBtn.ContentMode = UIViewContentMode.ScaleAspectFit;
			_prevMonthBtn.Tag = 0;
			//_prevMonthBtn.TouchUpInside += OnArrowTouchUp;
			//By vishal
			this.AddSubview(_prevMonthBtn);

			_nextMonthBtn = new UIButton(new CGRect((UIScreen.MainScreen.Bounds.Width - X_BORDER_OFFSET - ARROW_BTN_WIDTH), _monthNameLabel.Frame.Y, ARROW_BTN_WIDTH, _monthNameLabel.Frame.Height));
			_nextMonthBtn.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			_nextMonthBtn.ContentMode = UIViewContentMode.ScaleAspectFit;
			_nextMonthBtn.SetImage(UIImage.FromFile("calendar-next-black.png"), UIControlState.Normal);
			_nextMonthBtn.Tag = 1;
			//_nextMonthBtn.TouchUpInside += OnArrowTouchUp;
			this.AddSubview(_nextMonthBtn);

		}

		public  void OnArrowTouchUp(object sender)
		{
			var btn = sender as UIButton;
			if (btn.Tag == 0) //prev
			{

				var newmonth = _currentDate.AddMonths(-1);
				if (_minDate < newmonth)
				{
					ChangeMonth(newmonth);
					_selectMonthSlider.Value--;

					//by vishal
					if (PreviousMonth != null)
					{
						PreviousMonth(newmonth);

					}

				}

			}
			else //next
			{

				var newmonth = _currentDate.AddMonths(1);
				if (newmonth < _maxDate)
				{
					ChangeMonth(newmonth);
					_selectMonthSlider.Value++;
					//by vishal
					if (NextMonth != null)
					{
						NextMonth(newmonth);

					}

				}

			}

		}

		//add the name of the days at the top under the month name
		void AddWeekDays()
		{
			var i = 0;
			foreach (var d in DateTimeFormatInfo.CurrentInfo.DayNames)
			{


				UILabel daylbl = new UILabel(new CGRect(i * _singleDayFrameSize + _borderOffset, Y_BORDER_OFFSET, _singleDayFrameSize, DAY_NAME_HEIGTH + 15));
				daylbl.Text = d.Substring(0, 1);
				if (NSLocale.PreferredLanguages[0] == "ja") //japanese only display 1 character
					daylbl.Text = d.Substring(0, 1);

				daylbl.TextAlignment = UITextAlignment.Center;
				//daylbl.TextColor =  _fontColor;
				//by vishal
				daylbl.TextColor = UIColor.White;

				daylbl.Font = UIFont.FromName("HelveticaNeue-Light", 20f);

				if (UIScreen.MainScreen.Bounds.Height == IPHONE_5_HEIGHT)
					daylbl.Font = UIFont.FromName("HelveticaNeue", 17f);

				i++;
				this.AddSubview(daylbl);
				daylbl.BackgroundColor = UIColor.DarkGray;
			}
		}

		/// <summary>
		/// generates the slider used to browse months
		/// </summary>
		/// <param name="minDate">the min date of the calendar, this will be the min value of the slider</param>
		/// <param name="maxDate">the max date of the calendar, this will be the max value of the slider</param>
		void GenerateSlider(DateTime minDate, DateTime maxDate)
		{


			nfloat yPos = _calendarHeigth + Y_BORDER_OFFSET;

			_selectMonthSlider = new UISlider(new CGRect(X_BORDER_OFFSET, yPos, UIScreen.MainScreen.Bounds.Width - X_BORDER_OFFSET * 2, SLIDER_HEIGTH));
			//_selectMonthSlider.BackgroundColor = UIColor.LightGray;
			_selectMonthSlider.TintColor = _fontColor;
			_selectMonthSlider.MaximumTrackTintColor = UIColor.FromRGB(230, 230, 230);


			//calculate the number of months between the two dates
			var numOfMonths = ((maxDate.Year - minDate.Year) * 12) + maxDate.Month - minDate.Month;

			_selectMonthSlider.MinValue = 0;
			_selectMonthSlider.MaxValue = numOfMonths;

			//calculate where the slider is positioned according to the current month
			var currentMonthNum = ((maxDate.Year - _currentDate.Year) * 12) + maxDate.Month - _currentDate.Month;
			currentMonthNum = numOfMonths - currentMonthNum;
			_selectMonthSlider.Value = (float)currentMonthNum;

			_selectMonthSlider.ValueChanged += OnSliderValueChanged;

			this.AddSubview(_selectMonthSlider);
			_selectMonthSlider.Hidden = true;
		}

		void OnSliderValueChanged(object sender, EventArgs e)
		{
			//redrawing the month view
			var currentmonth = _minDate.AddMonths((int)_selectMonthSlider.Value);
			ChangeMonth(currentmonth);
		}

		//when the month is changed using the arrows, to move the slider according to the month
		void UpdateSliderValueFromDate(DateTime date)
		{
			var numOfMonths = ((_currentDate.Year - _minDate.Year) * 12) + _currentDate.Month - _minDate.Month;
			_selectMonthSlider.Value = (float)numOfMonths;
		}

		void ChangeMonth(DateTime month)
		{
			GenerateSingleMonth(month);
			UpdateMonthName(month, false);
			_currentDate = month;
		}

		//a day has been selected (tap) in the calendar
		void DayHasBeenSelected(int day)
		{
			_currentDate = new DateTime(_currentDate.Year, _currentDate.Month, day);
			//Console.WriteLine("Selected day: " +_currentDate.ToString("d"));

			UpdateMonthName(_currentDate, true);

			if (OnDaySelected != null)
			{
				OnDaySelected(_currentDate,ListofSelectedDates);

			}
		}

		//Generate a month view
		void GenerateSingleMonth(DateTime monthDate)
		{
			//clean the month view if it's a redraw
			if (_monthView.Superview != null)
			{
				foreach (var subview in _monthView.Subviews)
				{
					subview.RemoveFromSuperview();
				}
			}

			var daysInMonth = DateTime.DaysInMonth(monthDate.Year, monthDate.Month);
			var firstDay = new DateTime(monthDate.Year, monthDate.Month, 1).DayOfWeek;
			var weekdayOfFirst = (int)firstDay; //the name of the first day of the current month

			var position = weekdayOfFirst + 1;
			var line = 0;

			// draw the days
			for (int i = 1; i <= daysInMonth; i++)
			{
				var viewDay = new DateTime(monthDate.Year, monthDate.Month, i);

				var dayView = new CalendarDayView
				{
					Frame = new CGRect((position - 1) * _singleDayFrameSize + _borderOffset, (line * _singleDayFrameSize) + Y_BORDER_OFFSET, _singleDayFrameSize, _singleDayFrameSize),
					Today = (DateTime.Now.Date == viewDay.Date),
					Text = i.ToString(),
					Date = viewDay,

					Active = true,
					Tag = i,
					Selected = (i == DateTime.Now.Date.AddDays(1).Day)

				};

				//by vishal
				if (DateTime.Now.Date == viewDay.Date)
				{
					dayView.BackgroundColor = UIColor.Green;
				}
				dayView.AddGestureRecognizer(new UITapGestureRecognizer(sw =>
				{

					//by vishal
					#region
					if (DateTime.Now.Date <= viewDay.Date)
					{
						if (!ProcessMultipleSelection(ListofSelectedDates, (int)dayView.Tag))
						{
							ListofSelectedDates.Add((int)dayView.Tag);
							DayHasBeenSelected((int)dayView.Tag);
							dayView.BackgroundColor = UIColor.Red;
						}
						else
						{
							dayView.BackgroundColor = UIColor.White;
							if (DateTime.Now.Date == viewDay.Date)
							{
								dayView.BackgroundColor = UIColor.Green;
							}
							DayHasBeenSelected((int)dayView.Tag);
						}
					}
					#endregion
				}));

				_monthView.Add(dayView);

				position++;
				if (position > DAYS_IN_A_WEEK)
				{
					position = 1;
					line++;
				}
			}

		}
		bool ProcessMultipleSelection(List<int> list, int tag)
		{
			bool flag = false;
			try
			{
				if (list != null)
				{
					var count = list.Count;

					for (int i = 0; i < count; i++)
					{
						if (list[i] == tag)
						{
							flag = true;
							list.Remove(list[i]);
							ListofSelectedDates = list;
						}

					}
				}
			}
			catch (Exception ex)
			{

			}
			return flag;
		}

	}

	public class CalendarDayView : UIView
	{
		string _text;
		bool _active, _today, _selected;
		DateTime _date;

		public string Text { get { return _text; } set { _text = value; SetNeedsDisplay(); } }
		public bool Active { get { return _active; } set { _active = value; SetNeedsDisplay(); } }
		public bool Today { get { return _today; } set { _today = value; SetNeedsDisplay(); } }
		public bool Selected { get { return _selected; } set { _selected = value; SetNeedsDisplay(); } }
		public DateTime Date { get { return _date; } set { _date = value; SetNeedsDisplay(); } }

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			//BackgroundColor = UIColor.Green;
			UILabel text = new UILabel(new CGRect(0, 0, Frame.Width, Frame.Height));
			text.Text = _text;
			//text.BackgroundColor = UIColor.FromRGB (0, 121, 186);
			text.TextAlignment = UITextAlignment.Center;
			text.Font = UIFont.FromName("HelveticaNeue-Light", 20f);
			if (UIScreen.MainScreen.Bounds.Height == 568)
				text.Font = UIFont.FromName("HelveticaNeue-Light", 17f);


			text.TextColor = UIColor.Black;



			this.Add(text);
		}


	}

}
