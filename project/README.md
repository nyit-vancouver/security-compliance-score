# Project Title
Cloud Automated Security Audit

# Description
This project is to develop an automated security audit application based on Azure, which can be used to report security check results against pre-configured compliance policies, including user identity, data, and end-point devices. This App will provide recommendations for organizations for applying suggested security mechanisms, deploying appropriate security policies, or addressing the improvement actions.

# Why the project is useful
This project provides a consolidated tool to perform cloud security audit automatically. 

# User Authentication 
The App works based on Microsoft cloud Azure. It is needed to create and configure the client secrete from file "appsettings.json". For example: 
{
  "settings": {
    "clientId": "f9f12bec-7f61-4cfc-81ca-c423f69a0ccc",
    "clientSecret": "lWh8Q~***",
    "tenantId": "240d0a1a-***",
    "authTenant": "common"
  }
}
Once the configuration is correct, users can run the app without any manual authentication. Reference: https://learn.microsoft.com/en-us/azure/active-directory/develop/msal-authentication-flows

# Build and run 
Project codes are written in C#. Run from windows command line or PowerShell:
dotnet run .NET Graph Tutorial

1) Once launch the App
Security and compliance score - CLI
Please choose one of the following options:
0. Exit
1. Authentication
10. Password Authentication
2. Conditional Access
20. Require MFA for Admins
21. Block Legacy Authentication
22. Blocking risky sign-in behaviors
3. Secure Scores
30. Role Overlap
31. One Admin
32. Legacy Authentication
33. Admin MFA
4. Policies
40. Security Defaults
41. Self Service Password Reset
5. Domains
50. TXT spf
51. TXT dkim
52. TXT dmarc
100. Export CSV

2) Specify a number, e.g. 21
21
Conditional Access - Block legacy authentication : PASS

3) Exit the program with input 0

# Who maintians and contributes to the project
Jing (Jenny) Li, Nitin Tutlani, Samuel William Almeida 
