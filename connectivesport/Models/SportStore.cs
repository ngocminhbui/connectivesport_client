﻿using System.Collections.Generic;

namespace connectivesport
{

	public class XLocation
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Northeast
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Southwest
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}

	public class Viewport
	{
		public Northeast northeast { get; set; }
		public Southwest southwest { get; set; }
	}

	public class Geometry
	{
		public XLocation location { get; set; }
		public Viewport viewport { get; set; }
	}

	public class OpeningHours
	{
		public bool open_now { get; set; }
		public List<object> weekday_text { get; set; }
	}

	public class Photo
	{
		public int height { get; set; }
		public List<string> html_attributions { get; set; }
		public string photo_reference { get; set; }
		public int width { get; set; }
	}

	public class SportStore
	{
		public Geometry geometry { get; set; }
		public string icon { get; set; }
		public string id { get; set; }
		public string name { get; set; }
		public OpeningHours opening_hours { get; set; }
		public List<Photo> photos { get; set; }
		public string place_id { get; set; }
		public double rating { get; set; }
		public string reference { get; set; }
		public string scope { get; set; }
		public List<string> types { get; set; }
		public string vicinity { get; set; }
	}
	public class Gym
	{
		public Geometry geometry { get; set; }
		public string icon { get; set; }
		public string id { get; set; }
		public string name { get; set; }
		public OpeningHours opening_hours { get; set; }
		public List<Photo> photos { get; set; }
		public string place_id { get; set; }
		public double rating { get; set; }
		public string reference { get; set; }
		public string scope { get; set; }
		public List<string> types { get; set; }
		public string vicinity { get; set; }
	}
}