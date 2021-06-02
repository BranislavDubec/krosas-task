# krosas-task

# Pred spustením
Vytvorenie databázy; je dostupný skript vygenerovaný pomocou **microsoft sql server management studio**.<br/>
Upraviť súbor **appsetting.json**; v tomto súbore je treba nastaviť **DefaultConnectionString** na vytvorenú databázu v zariadení.<br/>

# Funkčnosť
Na vytvorenie objektu pomocou **post** je potrebné zadať všetky informácie vrátane id.<br/>
**Put** treba zadať id, a atribúty na zmenenie; netreba zadávať všetky hodnoty.<br/>
**Delete** zmaže len v prípade, že sa nenachádza objekt ako FK v inom objekte.<br/>
Return hodnoty 400 v prípade zlého requestu, 404 ak sa nenašlo ID hľadaného objektu.<br/>

# Url

/api/zamestnanecs - /api/zamestnanecs/{id} <br/>
/api/oddelenies - /api/oddelenies/{id} <br/>
/api/firmas - /api/firmas{id}<br/>
/api/divizias - /api/divizias/{id}<br/>
/api/projekts - /api/projekts/{id}<br/>
