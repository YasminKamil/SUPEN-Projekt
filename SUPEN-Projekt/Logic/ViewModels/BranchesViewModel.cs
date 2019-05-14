using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	//En vymodell som håller id och en lista på branscher
	public class BranchesViewModel {
		public int Id { get; set; }
		public IEnumerable<Branch> Branches { get; set; }
	}
}