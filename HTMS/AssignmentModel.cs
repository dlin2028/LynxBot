using System;
using System.Collections.Generic;
using System.Text;

namespace HTMS
{

    public class AssignmentModel
    {
        public Assignment[] assignment { get; set; }
        public int total { get; set; }
        public Links links { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
    }

    public class Assignment
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string due { get; set; }
        public string grading_scale { get; set; }
        public string grading_period { get; set; }
        public string grading_category { get; set; }
        public string max_points { get; set; }
        public string factor { get; set; }
        public string is_final { get; set; }
        public string show_comments { get; set; }
        public string grade_stats { get; set; }
        public string allow_dropbox { get; set; }
        public string allow_discussion { get; set; }
        public int published { get; set; }
        public string type { get; set; }
        public long grade_item_id { get; set; }
        public int available { get; set; }
        public int completed { get; set; }
        public int dropbox_locked { get; set; }
        public int grading_scale_type { get; set; }
        public bool show_rubric { get; set; }
        public string display_weight { get; set; }
        public string folder_id { get; set; }
        public string assignment_type { get; set; }
        public string web_url { get; set; }
        public string last_updated { get; set; }
        public string completion_status { get; set; }
        public Links1 links { get; set; }
    }

    public class Links1
    {
        public string self { get; set; }
    }

}
