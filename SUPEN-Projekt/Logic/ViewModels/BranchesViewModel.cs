using SUPEN_Projekt.Models;
using System.Collections.Generic;

namespace SUPEN_Projekt.Logic.ViewModels {
	public class BranchesViewModel {
		public int Id { get; set; }
		public IEnumerable<Branch> Branches { get; set; }
	}
}