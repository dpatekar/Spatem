$contexts = @(
			@('ConfigurationDbContext','IS4 Configuration Store','Configuration'),
			@('PersistedGrantDbContext','IS4 Operational Store','Operational')
			)

$contextChoices = @($i=0;$contexts | %{New-Object System.Management.Automation.Host.ChoiceDescription "&$i`b$($_[0])", $_[1]; $i++})
$contextDecision = $Host.UI.PromptForChoice('Choose DB context','Migration is DBContext specific', $contextChoices, 0)
$context = $contexts[$contextDecision][0]
$outDir = $contexts[$contextDecision][2]

Write-Host 'Listing existing migrations...'

dotnet ef migrations list --context $context

$confirm = New-Object Collections.ObjectModel.Collection[Management.Automation.Host.ChoiceDescription]
$confirm.Add((New-Object Management.Automation.Host.ChoiceDescription -ArgumentList '&Yes'))
$confirm.Add((New-Object Management.Automation.Host.ChoiceDescription -ArgumentList '&No'))

$decision = $Host.UI.PromptForChoice('Generating new migration...','Are you sure you want to proceed?', $confirm, 1)
if ($decision -eq 0) {
  Write-Host 'Confirmed'
  $MigrationName = Read-Host -Prompt 'Enter a name for new migration'
  dotnet ef migrations add $MigrationName --context $context --output-dir Migrations/$outDir
} else {
  Write-Host 'Cancelled'
}

$decision = $Host.UI.PromptForChoice('Applying latest migration...','Are you sure you want to proceed?', $confirm, 1)
if ($decision -eq 0) {
  Write-Host 'Confirmed'
  dotnet ef database update --context $context
} else {
  Write-Host 'Cancelled'
}