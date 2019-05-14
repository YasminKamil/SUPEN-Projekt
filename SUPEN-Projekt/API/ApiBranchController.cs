using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Controllers {
	public class ApiBranchController : ApiController {
		//IUnitOfWork följer depedency injection. Kommunicerar med repository interfaces
		//Se Repository UnitOfWork för implementation
		IUnitOfWork uw;

		public ApiBranchController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Returnerar en specifik branch som finns lagrad i databasen
		[Route("api/GetBranch/{inBranchId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBranch(int inBranchId) {
			BranchViewModel bVM = new BranchViewModel();
			//Anropar på GetBranch() från repository för att hämta den specifika branschen
			bVM.branch = await uw.Branches.GetBranch(inBranchId);

			if (bVM == null) {
				return NotFound();
			}
			return Ok(bVM);
		}

		//Returnerar alla branscher finns lagrade i databasen
		[Route("api/GetBranches")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBranches() {
			BranchesViewModel list = new BranchesViewModel();
			//Listan av brancher tilldelas ett värde av branscher genom att anropa på metoden GetBranches() från repository
			list.Branches = await uw.Branches.GetBranches();

			if (list == null) {
				return NotFound();
			}
			return Ok(list);
		}

		//Returnerar alla relationer en bransch har till andra branscher som finns lagrade i databasen
		[Route("api/GetBranchRelations/{inBookingSystemId}/{inServiceId}/{inBranchId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBranchRelations(int inBookingSystemId, int inServiceId, int inBranchId) {
			var bs = await uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingBranchBranchRelationViewModel bsSBVMBBR = new BookingSystemServiceBookingBranchBranchRelationViewModel();
			bsSBVMBBR.bookingSystem = bs;
			bsSBVMBBR.service = bsSBVMBBR.bookingSystem.Services.Single(x => x.ServiceId == inServiceId);
			bsSBVMBBR.branch = await uw.Branches.Get(inBranchId);
			//Tilldelar variabeln ett värde med bransch relationer genom att anropa på GetBranchRelations() metoden från repository
			var branchRelations = await uw.Branches.GetBranchRelations(inBranchId);
			bsSBVMBBR.branchRelations = branchRelations.ToList();

			if (bsSBVMBBR == null) {
				return NotFound();
			}
			return Ok(bsSBVMBBR);
		}

		//Skapar en ny relation mellan branscherna om den inte finns, finns den så läggs det på ett klick i relationen mellan branscherna
		[Route("api/UpdateBranchRelation/{inBranchAId}/{inBranchBId}")]
		[HttpPost]
		public IHttpActionResult UpdateBranchRelation(int inBranchAId, int inBranchBId) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}
			//Anropar på CreateBranchRelation() från repository för att skapa en ny relation
			uw.Branches.CreateBranchRelation(inBranchAId, inBranchBId);
			uw.Complete();
			return Ok();
		}
	}
}
