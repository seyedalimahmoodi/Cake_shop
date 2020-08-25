using System;
using System.Collections.Generic;
using System.Text;
using Cakeshop.Core.DTOs;
using Cakeshop.Core.DTOs.Users;
using Cakeshop.DataLayer.Entities.User;


namespace Cakeshop.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool IsExistUserNameForEdit(string userName, string oldusername);
        bool IsExistEmailForEdit(string email,string username);
        int AddUser(User user);
        int GetUserIdByUserName(string userName);
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User LoginUser(LoginViewModel login);
        User GetUserByUserName(string username);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        bool ActiveAccount(string activeCode);
        InformationUserViewModel GetUserInformation(string username);
        InformationUserViewModel GetUserInformation(int userId);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        EditProfileViewModel GetDataForEditProfileUser(string name);
        void EditProfile(string username, EditProfileViewModel profile);
        bool CompareOldPassword(string oldPassword, string username);
        void ChangeUserPassword(string userName, string newPassword);

        void AddUserLocation(UserLocation location);
        List<UserLocation> Locations();

        int GetAmountCarts(string username);
        List<CartViewModel> GetCartUser(string userName);
        int ChargeCart(string userName, int amount, string description, bool isPay = false);
        int AddCart(Cart cart);
        Cart GetCartByCartId(int cartId);
        void UpdateCart(Cart cart);

        UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "");
        EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFromAdmin(EditUserViewModel editUser);
    }
}
