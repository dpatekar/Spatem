$context = 'ApplicationDbContext'
$outDir = 'ApplicationData'

Write-Host 'Listing existing migrations...'

$StartupProject = '../Spatem.Api'
dotnet ef --startup-project $StartupProject migrations list --context $context

$confirm = New-Object Collections.ObjectModel.Collection[Management.Automation.Host.ChoiceDescription]
$confirm.Add((New-Object Management.Automation.Host.ChoiceDescription -ArgumentList '&Yes'))
$confirm.Add((New-Object Management.Automation.Host.ChoiceDescription -ArgumentList '&No'))

$decision = $Host.UI.PromptForChoice('Generating new migration...','Are you sure you want to proceed?', $confirm, 1)
if ($decision -eq 0) {
  Write-Host 'Confirmed'
  $MigrationName = Read-Host -Prompt 'Enter a name for new migration'
  dotnet ef --startup-project $StartupProject migrations add $MigrationName --context $context --output-dir Migrations/$outDir
} else {
  Write-Host 'Cancelled'
}

$decision = $Host.UI.PromptForChoice('Applying latest migration...','Are you sure you want to proceed?', $confirm, 1)
if ($decision -eq 0) {
  Write-Host 'Confirmed'
  dotnet ef --startup-project $StartupProject database update --context $context
} else {
  Write-Host 'Cancelled'
}