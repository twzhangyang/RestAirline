Param(
    [parameter(Mandatory=$true)][string]$resourceGroupName,
    [parameter(Mandatory=$true)][string]$location,
    [parameter(Mandatory=$true)][string]$serviceName,
    [parameter(Mandatory=$true)][string]$dnsNamePrefix,
    [parameter(Mandatory=$true)][string]$registryName,
    [parameter(Mandatory=$false)][int]$nodeCount=1,
    [parameter(Mandatory=$false)][string]$nodeVMSize="Standard_D2_v2"
)

# Create resource group
Write-Host "Creating Azure Resource Group..." -ForegroundColor Yellow
az group create --name=$resourceGroupName --location=$location

# Create Azure Container Registry
Write-Host "Creating Azure Container Registry..." -ForegroundColor Yellow
az acr create -n $registryName -g $resourceGroupName -l $location  --admin-enabled true --sku Basic

# Create kubernetes cluster in AKS
Write-Host "Creating Kubernetes cluster in AKS..." -ForegroundColor Yellow
az aks create --resource-group=$resourceGroupName --name=$serviceName --dns-name-prefix=$dnsNamePrefix --generate-ssh-keys --node-count=$nodeCount --node-vm-size=$nodeVMSize

# Retrieve kubernetes cluster configuration and save it under ~/.kube/config
Write-Host "Getting Kubernetes config..." -ForegroundColor Yellow
az aks get-credentials --resource-group=$resourceGroupName --name=$serviceName

# Show ACR credentials
Write-Host "ACR credentials" -ForegroundColor Yellow
az acr credential show -n $registryName
