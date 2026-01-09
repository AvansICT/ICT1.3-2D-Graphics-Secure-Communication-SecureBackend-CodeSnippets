# Aure ARM script - Create a free Azure App Service & Azure SQL database in a single deployment 🐱‍🏍
You can deploy [this script](./azure/azure_arm_free_app_and_database.json) directly using the Azure Portal: 
1. Navigate to: [Deploy a custom template](https://portal.azure.com/#create/Microsoft.Template)  
2. Load file or 'Build your own' and copy paste
3. Create (or choose) Resource Group
4. Provide the required parameters:  
   - Student Number 👈 Will be used to prefix the resource names
   - SQL Administrator Credentials 
   - The Home IP address you would like to allow to access the resources (you can add more in the portal)
5. Press Create

After the deployment has succeeded, navigate to output parameters and save these values. You need them later when you setup the GitHub Workflow.

** Notes **
- The free **SQL Server database** is **not available** in `westeurope`, so the script defaults to `northeurope`.  
- The SQL Server **uses SQL Authentication only**, as setting up Entra ID authentication through ARM templates is not straightforward.  

# GitHub Workflow Demystified
The [GitHub workflow in the starter template repository](https://github.com/AvansICT/ICT1.3-2D-Graphics-Secure-Communication-SecureBackend-StarterTemplate/blob/main/.github/workflows/github_workflow_simplified.yaml) takes the outputs from the Azure ARM template and the API project folder and uses it to create a simple CI/CD workflow through GitHub actions.

The following secrets are required to run this worflow
* AZURE_WEBAPP_NAME 👉 The name of the web application in Azure (output from the ARM template)
* AZURE_WEBAPP_PUBLISH_PASSWORD 👉 The publishing user password (output from the ARM template)
* AZURE_WEBAPP_PUBLISH_USERNAME 👉 The publishing user name (output from the ARM template)
* WEBAPI_PROJECT_FOLDER 👉 The name of the project that needs to be published

The secrets are used to construct a publishing profile. Unfortunately it is not possible to extract the full publish profile through the ARM template.

** Notes **
* It checks if the secrets are available, if not, cancels the flow
* It defaults to DOTNET_CORE_VERSION: 10.0.x so it expects a .NET 10 project

## Create additional SQL user
Execute [this script](./sql/create_database_user.sql) on the Azure SQL Database to create a contained SQL database user with only read/write access.
