// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace NuGetGallery
{
    public static class ServicesConstants
    {
        public const string CurrentUserOwinEnvironmentKey = "nuget.user";

        internal const string UserAgentHeaderName = "User-Agent";

        // X-NuGet-Client-Version header was deprecated and replaced with X-NuGet-Protocol-Version header
        // It stays here for backwards compatibility
        public const string ClientVersionHeaderName = "X-NuGet-Client-Version";
        public const string NuGetProtocolHeaderName = "X-NuGet-Protocol-Version";

        public const string DevelopmentEnvironment = "Development";
        public const string DevEnvironment = "Dev";
        public const string IntEnvironment = "Int";
        public const string ProdEnvironment = "Prod";

        public const string GitRepository = "git";

        public const string MarkdownFileExtension = ".md";
        public const string HtmlFileExtension = ".html";
        public const string JsonFileExtension = ".json";

        // Note: regexes must be tested to work in JavaScript
        // We do NOT follow strictly the RFCs at this time, and we choose not to support many obscure email address variants.
        // Specifically the following are not supported by-design:
        //  * Addresses containing () or []
        //  * Second parts with no dots (i.e. foo@localhost or foo@com)
        //  * Addresses with quoted (" or ') first parts
        //  * Addresses with IP Address second parts (foo@[127.0.0.1])
        internal const string EmailValidationRegexFirstPart = @"[-A-Za-z0-9!#$%&'*+\/=?^_`{|}~\.]+";
        internal const string EmailValidationRegexSecondPart = @"[A-Za-z0-9]+[\w\.-]*[A-Za-z0-9]*\.[A-Za-z0-9][A-Za-z\.]*[A-Za-z]";
        public const string EmailValidationRegex = "^" + EmailValidationRegexFirstPart + "@" + EmailValidationRegexSecondPart + "$";
        public static TimeSpan EmailValidationRegexTimeout = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Parameters for calculating account lockout period after 
        /// wrong password entry.
        /// </summary>
        public const double AccountLockoutMultiplierInMinutes = 10;
        public const double AllowedLoginAttempts = 10;

        public const string Sha1HashAlgorithmId = "SHA1";

        public const string ApiKeyHeaderName = "X-NuGet-ApiKey";

        /// <summary>
        /// Parameter for passing the cookie compliance permission.
        /// </summary>
        public const string CookieComplianceCanWriteAnalyticsCookies = "CanWriteAnalyticsCookies";

        public static class ContentNames
        {
            public const string LoginDiscontinuationConfiguration = "Login-Discontinuation-Configuration";
            public const string CertificatesConfiguration = "Certificates-Configuration";
            public const string SymbolsConfiguration = "Symbols-Configuration";
            public const string TyposquattingConfiguration = "Typosquatting-Configuration";
            public const string NuGetPackagesGitHubDependencies = "GitHubUsage.v1";
            public const string ABTestConfiguration = "AB-Test-Configuration";
            public const string CacheConfiguration = "Cache-Configuration";
            public const string QueryHintConfiguration = "Query-Hint-Configuration";
            public const string TrustedImageDomains = "Trusted-Image-Domains";
        }
    }
}
