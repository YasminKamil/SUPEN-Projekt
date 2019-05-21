using SUPEN_Projekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SUPEN_Projekt.Repositories
{
    //Interface för BranchRepository för att kunna återanvända metoderna i presentationslagret/API:er
    public interface IBranchRepository : IRepository<Branch>
    {
        void CreateBranchRelation(int branchA, int branchB);
        void AddClickToBranchRelation(int branchA, int branchB);
        Task<BranchRelation> GetBranchRelation(int branchA, int branchB);
        Task<Branch> GetBranch(int branchId);
        Task<IEnumerable<Branch>> GetBranches();
        Task<IEnumerable<BranchRelation>> GetBranchRelations(int branchA);
    }
}
