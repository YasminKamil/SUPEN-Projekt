using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUPEN_Projekt.Repositories
{
    public class BrancheRepository : Repository<Branch>, IBrancheRepository
    {
        public BrancheRepository(ApplicationDbContext context) : base(context)
        { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        //uppdaterar en relation med ett till klick
        public void AddClickToBranchRelation(int branchA, int branchB)
        {
            Branch fromBranch = ApplicationDbContext.Branches.Single(x=> x.BranchId == branchA);
            BranchRelation branchRelation = fromBranch.BranchRelations.Single(x=> x.Branch.BranchId == branchB);
            branchRelation.CountClick++;
        }

        //BranchA är den bransch som valdes först och branchB är den som valdes bland förslagen.
        public void CreateBranchRelation(int branchA, int branchB)
        {
            try
            {
            BranchRelation branchRelation = new BranchRelation();
            branchRelation.Branch = ApplicationDbContext.Branches.Single(x=>x.BranchId == branchB);
            branchRelation.CountClick = 1;

           Branch fromBranch = ApplicationDbContext.Branches.Single(x=>x.BranchId == branchA);

            if (fromBranch.BranchRelations.Count(x=> x.Branch.BranchId == branchB) == 0)
            {
                fromBranch.BranchRelations.Add(branchRelation);
            }
            else
            {
                AddClickToBranchRelation(branchA, branchB);
            }

            }
            catch (Exception e)
            {

                throw e;
            }

        }
        //Returnerar en BranchRelation
        public BranchRelation GetBranchRelation(int branchA, int branchB)
        {
            Branch fromBranch = ApplicationDbContext.Branches.Single(x => x.BranchId == branchA);
            return fromBranch.BranchRelations.Single(x=> x.Branch.BranchId == branchB);
        }
        //Returnerar en BranchRelation
        public IEnumerable<BranchRelation> GetBranchRelations(int branchA)
        {
            return ApplicationDbContext.Branches.Single(x=>x.BranchId == branchA).BranchRelations;
        }


    }

}