<Query Kind="Statements">
  <Connection>
    <ID>ec964329-59a6-4acd-b775-1dc7bd949740</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>10.69.16.236\MSSQL</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>GlobalIdentity</Database>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXF5tYe8WukKuWZ0VNMWZsAAAAAACAAAAAAADZgAAwAAAABAAAAB+lbsijlqSVrcJOTC0iVoTAAAAAASAAACgAAAAEAAAAN5WMbfWkaKWq874ToxUczkQAAAAlnSZhpc8yscvLTr+LEb4fBQAAAARCF2wsRqRCfs+2cgIlkdSRCPnDA==</Password>
  </Connection>
</Query>

var result = GlobalIdentifiers
			.GroupJoin
			(IdentityMessages, 
				i => i.Id, 
				m => m.GlobalIdentifierId, 
				(i,im) => new 
				{
					Name = i.Name,
					ReceiptTime = im.Max(x => x.ReceiptTime)
				}	
			).OrderBy(n => n.Name)
			.Where(x => x.Name != "Неизвестный идентификатор");

result.Dump();

//IdentityMessages.Where(x => x.GlobalIdentifier.Id == 2 ).OrderByDescending(x => x.ReceiptTime).Take(1)

/*  var queryResult = _Database.GlobalIdentifiers
                    .GroupJoin
                    (_Database.IdentityMessages,
                        i => i.Id,
                        m => m.GlobalIdentifier.Id,
                        (i, im) => new
                        {
                            Name = i.Name,
                            ReceiptTime = im.Max(x => x.ReceiptTime)
                        }
                    );*/