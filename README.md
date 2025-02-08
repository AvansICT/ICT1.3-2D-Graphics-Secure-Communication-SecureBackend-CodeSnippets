# ICT1.3 - 2D Graphics & Secure Communication - Secure Backend - Code Snippets
This repository contains publicly available code snippets & scripts. 

## Aure ARM script - Create a free Azure App Service & Azure SQL database in a single deployment 🐱‍🏍
You can deploy [this script](./azure/azure_arm_free_app_and_database.json) directly using the Azure Portal: 
1. Navigate to: [Deploy a custom template](https://portal.azure.com/#create/Microsoft.Template)  
2. Load file or 'Build your own' and copy paste
3. Create (or choose) Resource Group
4. Provide the required parameters:  
   - Student Number 👈 Will be used to prefix the resource names
   - SQL Administrator Credentials 
   - The IP address you would like to allow to access the resources (you can add more in the portal)
5. Press Create

After the deployment has succeeded, navigate to output parameters and save these values. You need them later when you setup the GitHub Workflow.

#### Notes  
- The free **SQL Server database** is **not available** in `westeurope`, so the script defaults to `northeurope`.  
- The SQL Server **uses SQL Authentication only**, as setting up Entra ID authentication via ARM scripts is not straightforward.  
