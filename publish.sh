dotnet publish ./CryptoExchangeWrapper -r linux-x64 -c Release

# 
cp ./CryptoExchangeWrapper/bin/Release/net8.0/linux-x64/publish/*.* ~/f0cii/mojo-experiments/libs/
# 
cp ./CryptoExchangeWrapper/bin/Release/net8.0/linux-x64/publish/*.* ~/f0cii/trade-forge/libs/
