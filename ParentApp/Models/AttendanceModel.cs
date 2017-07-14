using System;
using System.Collections.Generic;

namespace ParentApp
{
	public class AttendanceModel
	{
		public int h_id { get; set; }
		public DateTime holiday_date { get; set; }
		public string holiday_name { get; set; }
		public int school_id { get; set; }
		public int a_id { get; set; }
		public DateTime date { get; set; }
		public List<AttendanceModel> arrayList { get; set; }

	}
	public class ListModel
	{
		public List<AttendanceModel> arrayList { get; set; }

	}

}
