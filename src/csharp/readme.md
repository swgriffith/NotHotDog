# NotHotDog

Login-AzureRMAccount

Get-AzureRmSubscription

Set-AzureRmContext -SubscriptionId <Subscription ID>

#Convert the secret into a secure string
$secretvalue = ConvertTo-SecureString "<vision api key value>" -AsPlainText -Force

#Store the secret in Key Vault
Set-AzureKeyVaultSecret -VaultName <KeyVaultName> -Name <Key Name> -SecretValue $secretvalue

Get-AzureKeyVaultSecret –VaultName <KeyVaultName>

