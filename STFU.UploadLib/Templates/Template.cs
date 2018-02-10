﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using STFU.UploadLib.Communication.Youtube.Serializable;
using STFU.UploadLib.Videos;

namespace STFU.UploadLib.Templates
{
	public class Template
	{
		private IList<PublishTime> publishTimes;

		private PrivacyStatus privacy;

		public int Id { get; internal set; }

		public string Name { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public Category Category { get; set; }

		public Language DefaultLanguage { get; set; }

		public string Tags { get; set; }

		[JsonConverter(typeof(EnumConverter))]
		public PrivacyStatus Privacy
		{
			get { return privacy; }
			set { privacy = value; }
		}

		[JsonConverter(typeof(EnumConverter))]
		public License License { get; set; }

		public bool IsEmbeddable { get; set; }

		public bool PublicStatsViewable { get; set; }

		public bool NotifySubscribers { get; set; }

		public bool AutoLevels { get; set; }

		public bool Stabilize { get; set; }

		public bool ShouldPublishAt { get; set; }

		public IList<PublishTime> PublishTimes
		{
			get
			{
				if (publishTimes == null)
				{
					publishTimes = new List<PublishTime>();
				}

				return publishTimes;
			}
			set
			{
				publishTimes = value;
			}
		}

		[JsonConstructor]
		public Template(int id, string name, Language defaultlanguage, Category category)
		{
			Id = id;
			Name = name;
			Privacy = PrivacyStatus.Private;
			Title = string.Empty;
			Description = string.Empty;
			Tags = string.Empty;
			NotifySubscribers = true;
			License = License.Youtube;
			DefaultLanguage = defaultlanguage;
			Category = category;
		}

		public static explicit operator Template(JToken v)
		{
			return JsonConvert.DeserializeObject<Template>(v.ToString());
		}

		public override string ToString()
		{
			return $"Id: {Id}, Name: {Name}";
		}

		public static Template Duplicate(Template template)
		{
			return new Template(template.Id, template.Name, template.DefaultLanguage, template.Category)
			{
				AutoLevels = template.AutoLevels,
				Description = template.Description,
				IsEmbeddable = template.IsEmbeddable,
				License = template.License,
				NotifySubscribers = template.NotifySubscribers,
				Privacy = template.Privacy,
				PublicStatsViewable = template.PublicStatsViewable,
				PublishTimes = new List<PublishTime>(template.PublishTimes),
				ShouldPublishAt = template.ShouldPublishAt,
				Stabilize = template.Stabilize,
				Tags = template.Tags,
				Title = template.Title,
			};
		}
	}
}
