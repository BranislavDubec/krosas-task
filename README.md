# krosas-task

# Pred spustením
Vytvorenie databázy; je dostupný skript vygenerovaný pomocou **microsoft sql server management studio**.
Upraviť súbor **appsetting.json**; v tomto súbore je treba nastaviť **DefaultConnectionString** na vytvorenú databázu v zariadení.

# Funkčnosť
Na vytvorenie objektu pomocou **post** je potrebné zadať všetky informácie vrátane id, až na telefon v tabuľke **zamestnanec**.
**Put** treba zadať id, a atribúty na zmenenie; netreba zadávať všetky hodnoty.
**Delete** zmaže len v prípade, že sa nenachádza objekt ako FK v inom objekte.
Return hodnoty 400 v prípade zlého requestu, 404 ak sa nenašlo ID hľadaného objektu.

# Url

/api/zamestnanecs - /api/zamestnanecs/{id}
/api/oddelenies - /api/oddelenies/{id}
/api/firmas - /api/firmas{id}
/api/divizias - /api/divizias/{id}
/api/projekts - /api/projekts/{id}
