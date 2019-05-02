using SUPEN_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        void CreateBranchRelation(int branchA, int branchB);
        void AddClickToBranchRelation(int branchA, int branchB);
        BranchRelation GetBranchRelation(int branchA, int branchB);
        IEnumerable<BranchRelation> GetBranchRelations(int branchA);
    }

}
