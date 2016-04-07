Dörrar, "TP", chests etc måste vara objects.

Ladda endast in filer från Sprite Mapparna

UnityTileID måste finnas.
ObjectIndex måste finnas på objects.
TeleportIndex måste finnas på dörrar man ska kunna gå genom.

Vi måste ta en titt på alla tiles, ge dem standardiserade namn och lägga in dem rätt i Unity. 
Inga dubbelfiler ok.
Inga dubbeltiles ok.

Dela upp dem i "entities" räcker. Ex Träd, Hus...


Objekt -> Allt som är på något sätt unikt. (Dörrar, chests, npc, mobs)
Tiles -> Allt som är likdant som sina medparter.



RedHouse Door, FunctioningDoor ---> Använd en parameter ur TMX för att sätta en bool om den ska göra något eller ej. Samma med evnt. chests osv.









Highest UnityTileIndex used; 241

ObjectIndex;
 