<Query Kind="Statements">
  <Connection>
    <ID>d5a41398-5251-4b21-83c0-c86e7a0fb64e</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="EF7Driver" PublicKeyToken="469b5aa5a4331a8c">EF7Driver.StaticDriver</Driver>
    <CustomAssemblyPath>E:\NET_Development\Repositories\GlobalIdentity\DatabaseClassGenerator\bin\Debug\net6.0\DatabaseClassGenerator.dll</CustomAssemblyPath>
    <CustomTypeName>DatabaseClassGenerator.GlobalIdentityContext</CustomTypeName>
    <DriverData>
      <UseDbContextOptions>false</UseDbContextOptions>
    </DriverData>
  </Connection>
</Query>

var resultQuery = from i in GlobalIdentifiers
                  select new { i.Id, i.Name } into resultSelect
                  join m in IdentityMessages on resultSelect.Id equals m.GlobalIdentifier.Id into joinResult
                  select new
                  {
                      Name = resultSelect.Name,
                      LastReceiptTime = joinResult.Max(t => t.ReceiptTime)
                  };

resultQuery.Dump();