using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wedding_Planner.Models
{
    public class AnalyticsViewModel
    {
        public int TotalBookings { get; set; }
        public double TotalRevenue { get; set; }
        public int TotalCustomers { get; set; }
        public int UpcomingEvents { get; set; }

        // Charts Data
        public List<string> EventNames { get; set; }
        public List<int> EventCounts { get; set; }
        public List<string> AmenityNames { get; set; }
        public List<int> AmenityCounts { get; set; }
        public List<BookingMaster> RecentBookings { get; set; }
        public int TotalComplaint { get;  set; }
        public int TotalContact { get;  set; }
        public int TotalFeedback { get;  set; }
        public int TotalEnquary { get; set; }
    }
}