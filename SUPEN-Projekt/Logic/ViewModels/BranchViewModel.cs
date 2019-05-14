using SUPEN_Projekt.Models;

namespace SUPEN_Projekt.Logic.ViewModels {
	//En vymodell som håller id och en branch 
	public class BranchViewModel {
		public int Id { get; set; }
		public Branch branch { get; set; }
	}
}