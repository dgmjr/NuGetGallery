// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NuGet.Services.Entities;

namespace NuGetGallery
{
    public class UserProfileModel
    {
        public UserProfileModel(User user, User currentUser, List<ListPackageItemViewModel> allPackages, int pageIndex, int pageSize, UrlHelper url, bool usesPaging, long totalDownloadCount, int packageCount)
        {

            User = user;
            Username = user.Username;
            EmailAddress = user.EmailAddress;
            UnconfirmedEmailAddress = user.UnconfirmedEmailAddress;
            IsLocked = user.IsLocked;
            AllPackages = allPackages;
            PackagePage = pageIndex;
            PackagePageSize = pageSize;
            
            if (usesPaging)
            {
                TotalPackages = packageCount;
                TotalPackageDownloadCount = totalDownloadCount;
                PagedPackages = AllPackages;
            }
            else
            {
                TotalPackages = allPackages.Count;
                TotalPackageDownloadCount = AllPackages.Sum(p => p.TotalDownloadCount);
                PagedPackages = AllPackages.Skip(PackagePageSize * pageIndex)
                    .Take(PackagePageSize).ToList();
            }

            PackagePageTotalCount = (TotalPackages + PackagePageSize - 1) / PackagePageSize;

            var pager = new PreviousNextPagerViewModel<ListPackageItemViewModel>(allPackages, pageIndex, PackagePageTotalCount,
                page => url.User(user, page));

            Pager = pager;

            CanManageAccount = ActionsRequiringPermissions.ManageAccount.CheckPermissions(currentUser, user) == PermissionsCheckResult.Allowed;
            CanViewAccount = ActionsRequiringPermissions.ViewAccount.CheckPermissions(currentUser, user) == PermissionsCheckResult.Allowed;
        }

        public int PackagePageTotalCount { get; private set; }
        public User User { get; private set; }
        public string Username { get; private set; }
        public string EmailAddress { get; private set; }
        public string UnconfirmedEmailAddress { get; set; }
        public bool HasEnabledMultiFactorAuthentication 
        {
            get
            {
                if (UserIsOrganization)
                {
                    var organization = (Organization)User;
                    return organization
                        .Members
                        .Select(x => x.Member)
                        .All(x => x.EnableMultiFactorAuthentication);
                }
                else
                {
                    return User.EnableMultiFactorAuthentication;
                }
            }
        }

        public bool IsLocked { get; set; }
        public ICollection<ListPackageItemViewModel> AllPackages { get; private set; }
        public ICollection<ListPackageItemViewModel> PagedPackages { get; private set; }
        public long TotalPackageDownloadCount { get; private set; }
        public int TotalPackages { get; private set; }
        public int PackagePage { get; private set; }
        public int PackagePageSize { get; private set; }
        public IPreviousNextPager Pager { get; private set; }
        public bool CanManageAccount { get; private set; }
        public bool CanViewAccount { get; private set; }

        public bool UserIsOrganization
        {
            get
            {
                return User is Organization;
            }
        }
    }
}
