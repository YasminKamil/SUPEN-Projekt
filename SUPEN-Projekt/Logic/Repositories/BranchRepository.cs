using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SUPEN_Projekt.Repositories
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext context) : base(context)
        { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
        //uppdaterar en relation med ett till klick
        public void AddClickToBranchRelation(int branchA, int branchB)
        {
            Branch fromBranch = ApplicationDbContext.Branches.Include(x=>x.BranchRelations).Single(x=> x.BranchId == branchA);
            //BranchRelation branchRelation = fromBranch.BranchRelations.Single(x=> x.BranchB.BranchId == branchB);
            BranchRelation branchRelation = fromBranch.BranchRelations.Single(x => Int32.Parse(x.branchBId2) == branchB);

            branchRelation.CountClick++;
            ApplicationDbContext.SaveChanges();
        }

        //BranchA är den bransch som valdes först och branchB är den som valdes bland förslagen.
        public void CreateBranchRelation(int branchA, int branchB)
        {
            try
            {

                Branch fromBranch = ApplicationDbContext.Branches.Include(x=>x.BranchRelations).Single(f => f.BranchId == branchA);
                //Branch fromBranch = ApplicationDbContext.Branches.Include(x=>x.BranchRelations.Select(s => s.BranchB)).Single(f=>f.BranchId == branchA);
                if (fromBranch.BranchRelations == null)
                {
                    fromBranch.BranchRelations = new List<BranchRelation>();
                }
            if (fromBranch.BranchRelations.Where(x=> x.branchBId2 == branchB.ToString()).Count() == 0) //       if (fromBranch.BranchRelations.Where(x=> x.BranchB.BranchId == branchB).Count() == 0)
                {
                    BranchRelation branchRelation = new BranchRelation();

                    
                    branchRelation.branchBId2 = branchB.ToString();
                    //branchRelation.BranchB = ApplicationDbContext.Branches.Single(x => x.BranchId == branchB);
                    //branchRelation.BranchBId = branchB;

                    branchRelation.CountClick = 1;
                //ApplicationDbContext.BranchRelations.Add(branchRelation);
                 //   ApplicationDbContext.SaveChanges();
                //fromBranch.BranchRelations.Add(branchRelation);
                    ApplicationDbContext.Branches.Single(x=>x.BranchId == branchA).BranchRelations.Add(branchRelation);
                    ApplicationDbContext.SaveChanges();
                }
            else
            {
                AddClickToBranchRelation(branchA, branchB);
            }
                ApplicationDbContext.SaveChanges();
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
            return fromBranch.BranchRelations.Single(x=> x.branchBId2 == branchB.ToString()); //fromBranch.BranchRelations.Single(x=> x.BranchB.BranchId == branchB);
        }
        //Returnerar en BranchRelation
        public IEnumerable<BranchRelation> GetBranchRelations(int branchA)
        {
            return ApplicationDbContext.Branches.Single(x=>x.BranchId == branchA).BranchRelations;
        }


    }

}