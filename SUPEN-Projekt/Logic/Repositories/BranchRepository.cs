using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SUPEN_Projekt.Repositories {
	//Ett repository för metoder som hanterar brancher.
	public class BranchRepository : Repository<Branch>, IBranchRepository {
		
		//Konstruktor med ApplicationDbContext som parametervärde
		public BranchRepository(ApplicationDbContext context) : base(context) { }

		//Gör ett anrop till ApplicationDbContext och returnerar contexten
		public ApplicationDbContext ApplicationDbContext {
			get { return Context as ApplicationDbContext; }
		}

		//Uppdaterar en relation med ett till klick mellan bransch A och B.
		public void AddClickToBranchRelation(int branchA, int branchB) {
			//Hämtar bransch A, som är den första valda branschen i bokningsflödet.
			Branch fromBranch = ApplicationDbContext.Branches.Include(x => x.BranchRelations).Single(x => x.BranchId == branchA);

			//Hämtar bransch B, baserat på den tidigare valda branschen.
			BranchRelation branchRelation = fromBranch.BranchRelations.Single(x => Int32.Parse(x.branchBId2) == branchB);

			//Lägger till ett klick och sparar i databasen
			branchRelation.CountClick++;
			ApplicationDbContext.SaveChanges();
		}

		/*Skapar en ny relation mellan branscher, där branchA är branschen som användaren väljer först och 
		branchB är den bransch som användaren väljer bland relevanta förslagen*/
		public void CreateBranchRelation(int branchA, int branchB) {
			try {
				//Hämtar första branschen och kontrollerar att den är branchA
				Branch fromBranch = ApplicationDbContext.Branches.Include(x => x.BranchRelations).Single(f => f.BranchId == branchA);

				//Om branchA inte finns skapas det en ny relation som kan göras mot branchA
				if (fromBranch.BranchRelations == null) {
					fromBranch.BranchRelations = new List<BranchRelation>();
				}

				/*Om det inte finns en tidigare relation mellan branchA och branchB skapas det en ny relation, och 1 poäng 
				 tilldelas efter att användaren har valt branchA efter branchB som sedan sparas i databasen*/
				if (fromBranch.BranchRelations.Where(x => x.branchBId2 == branchB.ToString()).Count() == 0) {
					BranchRelation branchRelation = new BranchRelation();
					branchRelation.branchBId2 = branchB.ToString();
					branchRelation.CountClick = 1;
					ApplicationDbContext.Branches.Single(x => x.BranchId == branchA).BranchRelations.Add(branchRelation);
					ApplicationDbContext.SaveChanges();
				}

				/*Om det finns en tidigare relation mellan branscherna kommer det att tilldelas 1 poäng för varje gång man 
				 väljer samma bransch A och därefter samma bransch B*/
				else {
					AddClickToBranchRelation(branchA, branchB);
				}
				ApplicationDbContext.SaveChanges();
			} catch (Exception e) {
				throw e;
			}
		}

		//Returnerar en branchrelation
		public BranchRelation GetBranchRelation(int branchA, int branchB) {
			Branch fromBranch = ApplicationDbContext.Branches.Single(x => x.BranchId == branchA);
			return fromBranch.BranchRelations.Single(x => x.branchBId2 == branchB.ToString());
		}

		//Returnerar en BranchRelation
		public IEnumerable<BranchRelation> GetBranchRelations(int branchA) {
			return ApplicationDbContext.Branches.Single(x => x.BranchId == branchA).BranchRelations;
		}

        //Retunerar alla bokningssystem finns lagrade
		public Branch GetBranch(int branchId) {
            return ApplicationDbContext.Branches.Include(y => y.BranchRelations).Single(x => x.BranchId == branchId);
        }

        public IEnumerable<Branch> GetBranches()
        {
            return ApplicationDbContext.Branches.Include(x => x.BranchRelations);
        }

    }

}