# Départ — prise de commande

Les couches Domaine, Application et Infrastructure, la console et les tests métier sont fournis et fonctionnels. Les compléter à nouveau n'est pas l'objet de l'activité. Le formulaire est volontairement vide : construire les contrôles et événements selon l'énoncé.

```bash
dotnet build S09E01_Commandes.slnx
dotnet test S09E01_Commandes.Portable.slnx
dotnet run --project S09E01_Commandes.Terminal
# Sous Windows :
dotnet run --project S09E01_Commandes.WinForms
```

La console démontre le total 22,00. Le lancement WinForms du départ affiche seulement une fenêtre à construire. Les noms des projets sont identiques à ceux de la remise et du corrigé. Aucun projet du TP01 ou d'un coéquipier n'est nécessaire. Les tests fournis ne prouvent pas que l'interface demandée est réalisée.

Configuration : mémoire par défaut, JSON disponible; modes manuel et cadriciel conservés dans Program. Écrire le test supplémentaire demandé et garder les tests fournis verts. Ne pas ajouter de règle métier ou de lecture de fichier dans FormCommande.
