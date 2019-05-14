using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	/*En vymodell som håller en lista på bokningssystem och deras distans, det tidigare valda bokningssystemet
	 och den första branschen som användaren har valt i bokningsflödet.*/
	public class BookingsWithDistanceViewModel {
		public IEnumerable<BookingSystemAndDistanceViewModel> BookingsWithDistance { get; set; }
		public BookingSystem SelectedBookingSystem { get; set; }
        public int branchAId { get; set; }

    }
}