# Départ — MVC, Observer et facturation

La console et WinForms démarrent avec le comportement de S9. Le domaine, les dépôts, la facturation pédagogique et leurs tests sont fonctionnels. Les canevas MVC/Observer contiennent des NotImplementedException : la compilation et les tests fournis ne prouvent pas que le travail est réalisé. Les compléter selon l’énoncé.

```bash
dotnet build S10E01_Facturation.slnx
dotnet test S10E01_Facturation.Portable.slnx
dotnet run --project S10E01_Facturation.Terminal
# Sous Windows :
dotnet run --project S10E01_Facturation.WinForms
```

La solution complète possède sept projets; la solution portable exclut WinForms. C# 14 / .NET 10, avec net10.0-windows pour la fenêtre. Le point de départ affiche une commande, sans graphe MVC/Observer relié. Le canevas de facture doit être abonné et ouvert par la composition.

Prévoir trois jalons dans la même solution : MVC à la séance 19, Observer à la séance 20, puis consolidation à la séance 21. Aucun projet externe, TP01 ou travail de coéquipier n’est nécessaire. Mémoire par défaut, JSON disponible; modes manuel/cadriciel à adapter et conserver.
