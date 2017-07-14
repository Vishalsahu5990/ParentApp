using System;
namespace ParentApp
{
	public class ChatModel
	{
		public int sender { get; set; }
		public string time { get; set; }
		public int status { get; set; }
		public int sender_id { get; set; }
		public int reciever_id { get; set; }
		public string msg { get; set; }
		public bool IsFromLocal { get; set; }

	}
}
