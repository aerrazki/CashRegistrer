# CashRegistrer

<h2>Design Pattern Strategy (Stratégie) pour la Gestion des Produits et des Offres</h2>

Mis en place le design pattern Strategy pour la gestion des produits dans un panier d'achats, en utilisant deux stratégies différentes : l'ajout manuel et l'ajout via le scan de code-barres.

<h3>Stratégies de Gestion des Produits :</h3>

<b>ScannedBarEntryStrategy :</b> Cette stratégie permet l'ajout de produits en scannant des codes-barres.

<b>ManualEntryStrategy</b> : Cette stratégie permet l'ajout de produits manuellement.

L'utilisation du modèle de conception Strategy permet de séparer la logique de gestion des produits du mode d'entrée, ce qui facilite l'ajout de nouveaux modes d'entrée à l'avenir sans perturber la logique du panier.

<h3>Offres Spéciales (Promotions) :</h3>

Le code met en place deux types d'offres spéciales pour les produits ajoutés au panier :

<b>BuyOneGetOneDiscount :</b> Pour chaque produit acheté, un produit gratuit est ajouté au panier. Si un nombre impair de produits est ajouté, un produit supplémentaire est offert, et le coût de la moitié du nombre impair - 1 est déduit.

<b>Buy10ItemsProductGetOneEuro :</b> Cette offre permet de déduire 1 euro pour chaque lot de 10 produits ajoutés au panier.

Ces offres spéciales sont appliquées à l'aide de la méthode GetDiscount de la classe ShoppingCart, qui calcule la valeur totale des remises applicables pour les produits actuellement dans le panier.
