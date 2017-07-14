using System;
using CoreLocation;
using MapKit;

namespace ParentApp
{
	public class CustomAnnotation: MKAnnotation
	{
		string title;
		CLLocationCoordinate2D coord;

		public CustomAnnotation(int id,string title, CLLocationCoordinate2D coord)
		{
			this.title = title;
			this.coord = coord;
			this.Id = id;
		}
		public int Id { get; }
		public override string Title
		{
			get
			{
				return title;
			}
		}


		public override CLLocationCoordinate2D Coordinate
		{
			get
			{
				return coord;
			}
		}
	}
}


