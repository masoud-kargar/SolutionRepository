using System.Collections.Generic;
using System.Security.Claims;

namespace AllServises.Claims {
    public static class ClaimStore {
        public static List<Claim> AllClaims = new List<Claim>() { 
            //سر دسته
            new Claim(ClaimTypeStpre.SubCategoryList,true.ToString()),
            new Claim(ClaimTypeStpre.SubCategoryAdd,true.ToString()),
            new Claim(ClaimTypeStpre.SubCategoryEdit,true.ToString()),
            new Claim(ClaimTypeStpre.SubCategoryDelete,true.ToString()),
            new Claim(ClaimTypeStpre.SubCategoryDetails,true.ToString()),
            
            //زیر دسته
            new Claim(ClaimTypeStpre.CategoryList,true.ToString()),
            new Claim(ClaimTypeStpre.CategoryAdd,true.ToString()),
            new Claim(ClaimTypeStpre.CategoryEdit,true.ToString()),
            new Claim(ClaimTypeStpre.CategoryDelete,true.ToString()),
            new Claim(ClaimTypeStpre.CategoryDetails,true.ToString()),
       
            //مدریت رول ها
            new Claim(ClaimTypeStpre.RoleList,true.ToString()),
            new Claim(ClaimTypeStpre.RoleAdd,true.ToString()),
            new Claim(ClaimTypeStpre.RoleEdit,true.ToString()),
            new Claim(ClaimTypeStpre.RoleDelete,true.ToString()),
            new Claim(ClaimTypeStpre.RoleDetails,true.ToString()),
            
            //مدریت کاربر ها
            new Claim(ClaimTypeStpre.UserList,true.ToString()),
            new Claim(ClaimTypeStpre.UserAdd,true.ToString()),
            new Claim(ClaimTypeStpre.UserEdit,true.ToString()),
            new Claim(ClaimTypeStpre.UserDelete,true.ToString()),
            new Claim(ClaimTypeStpre.UserDetails,true.ToString()),

            //policy
            new Claim(ClaimTypeStpre.ClaimsAdd,true.ToString()),
            new Claim(ClaimTypeStpre.ClaimsRemove,true.ToString()),
        };
    }
}
