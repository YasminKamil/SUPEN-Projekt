using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Controllers {
	public class ApiBranchController : ApiController {
		IUnitOfWork uw;

		public ApiBranchController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Returnerar alla relationer en branch har till andra brancher
		[Route("api/GetBranchRelations/{inBookingSystemId}/{inServiceId}/{inBranchId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBranchRelations(int inBookingSystemId, int inServiceId, int inBranchId) {
			var bs = await uw.BookingSystems.GetBookingSystem(inBookingSystemId);
			BookingSystemServiceBookingBranchBranchRelationViewModel bsSBVMBBR = new BookingSystemServiceBookingBranchBranchRelationViewModel();
			bsSBVMBBR.bookingSystem = bs;
			bsSBVMBBR.service = bsSBVMBBR.bookingSystem.Services.Single(x => x.ServiceId == inServiceId);
			bsSBVMBBR.branch = await uw.Branches.Get(inBranchId);
			bsSBVMBBR.branchRelations = uw.Branches.GetBranchRelations(inBranchId).ToList();

			if (bsSBVMBBR == null) {
				return NotFound();
			}
			return Ok(bsSBVMBBR);
		}

		//Lägger till relation om den inte finns, finns den så läggs det på ett click i relationen mellan brancher
		[Route("api/UpdateBranchRelation/{inBranchAId}/{inBranchBId}")]
		[HttpPost]
		public IHttpActionResult UpdateBranchRelation(int inBranchAId, int inBranchBId) {
			if (!ModelState.IsValid) {
				return BadRequest("Invalid data");
			}
			uw.Branches.CreateBranchRelation(inBranchAId, inBranchBId);
			uw.Complete();
			return Ok();
		}

		//Returnerar alla relationer en branch har till andra brancher
		[Route("api/GetBranch/{inBranchId}")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBranch(int inBranchId) {
			BranchViewModel bVM = new BranchViewModel();
			bVM.branch = await uw.Branches.GetBranch(inBranchId);

			if (bVM == null) {
				return NotFound();
			}
			return Ok(bVM);
		}

		[Route("api/GetBranches")]
		[HttpGet]
		public async Task<IHttpActionResult> GetBranches() {
			BranchesViewModel list = new BranchesViewModel();
			list.Branches = await uw.Branches.GetBranches();

			if (list == null) {
				return NotFound();
			}
			return Ok(list);
		}
	}
}
