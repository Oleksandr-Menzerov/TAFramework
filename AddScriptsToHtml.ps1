param (
    [string]$htmlFilePath,
    [string]$scriptFilePath
)

# Check if the HTML file exists
if (-not (Test-Path $htmlFilePath)) {
    Write-Host "HTML file not found at path: $htmlFilePath"
    exit
}

# Check if the script file exists
if (-not (Test-Path $scriptFilePath)) {
    Write-Host "JavaScript file not found at path: $scriptFilePath"
    exit
}

# Read the HTML file with UTF-8 encoding
$htmlContent = Get-Content -Path $htmlFilePath -Encoding utf8 -Raw

# Read the JavaScript file content
$scriptContent = Get-Content -Path $scriptFilePath -Raw

# Check if the body tag exists
if ($htmlContent -notmatch '</body>') {
    Write-Host "No </body> tag found in the HTML file."
    exit
}

# Insert the JavaScript just before the closing </body> tag
$htmlContent = $htmlContent -replace '</body>', "`n<script>`n$scriptContent`n</script>`n</body>"

# Save the modified content back to the HTML file with UTF-8 BOM encoding
[System.IO.File]::WriteAllText($htmlFilePath, $htmlContent, [System.Text.Encoding]::UTF8)

Write-Host "JavaScript has been successfully added to the HTML document."
