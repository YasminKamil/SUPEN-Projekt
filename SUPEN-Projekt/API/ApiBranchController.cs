using SUPEN_Projekt.Repositories;
using System.Linq;
using System.Web.Http;
using SUPEN_Projekt.Logic.ViewModels;

namespace SUPEN_Projekt.Controllers {
	public class ApiBranchController : ApiController {
		IUnitOfWork uw;

		public ApiBranchController(IUnitOfWork unitOfWork) {
			uw = unitOfWork;
		}

		//Hämtar alla lagrade Branscher 
		[Route("api/GetBranches")]
		[HttpGet]
		public IHttpActionResult GetBranches() {
			var Branches = uw.Branches.GetAll();
			BranchesViewModel list = new BranchesViewModel();

			list.Branches = Branches;

			if (list == null) {
				return NotFound();
			}

			return Ok(list);
		}

		//Returnerar alla relationer en branch har till andra brancher
		[Route("api/GetBranchRelations/{inBookingSystemId}/{inServiceId}/{inBranchId}")]
		[HttpGet]
		public IHttpActionResult GetBranchRelations(int inBookingSystemId,int inServiceId,int inBranchId) {

			var bs = uw.BookingSystems.GetBookingSystem(inBookingSystemId);
            BookingSystemServiceBookingBranchBranchRelationViewModel bsSBVMBBR = new BookingSystemServiceBookingBranchBranchRelationViewModel();

            bsSBVMBBR.bookingSystem = bs;
            bsSBVMBBR.service = bsSBVMBBR.bookingSystem.Services.Single(x => x.ServiceId == inServiceId);
            bsSBVMBBR.branch = uw.Branches.Get(inBranchId);
            bsSBVMBBR.branchRelations = uw.Branches.GetBranchRelations(inBranchId).ToList();

            if (bsSBVMBBR == null) {
				return NotFound();
			}
			return Ok(bsSBVMBBR);
		}

        //Lägger till relation om den inte finns, finns den så läggs det på ett click i relationen mellan brancher
        [Route("api/UpdateBranchRelation/{inBranchAId}/{inBranchBId}")]
        [HttpPost]
        public IHttpActionResult UpdateBranchRelation(int inBranchAId, int inBranchBId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            uw.Branches.CreateBranchRelation(inBranchAId, inBranchBId);
            uw.Complete();
            return Ok();
        }



    }
}
