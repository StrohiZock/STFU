﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STFU.UploadLib.Videos
{
	public class VideoResourceId
	{
		public DateTime endAt { get; set; }
		public string note { get; set; }
		public DateTime startAt { get; set; }
		public string videoId { get; set; }
		public DateTime videoPublishedAt { get; set; }

		//public bool shouldSerializedefaultLanguage { get { return defaultLanguage != null; } }
	}
}