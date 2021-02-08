# Codecool shop (sprint 1)

## Story

Everyone loves to buy and sell stuff but we need a shop for that! In this
project the goal is to build one the [most common type of websites on the
web](https://www.expertmarket.co.uk/web-design/different-types-of-websites): an
online eCommerce web-application, where users can browse products, add them into
a shopping cart, checkout items and make payments.

## What are you going to learn?

- how to create an ASP.NET Core web application using an in-memory database.
- how to use the `DAO` design pattern and dependency injection.
- how to log data in a specified format.


## Tasks

1. As a Developer, I want to have a version-controlled project, where a webserver serves requests. So that I can start developing in a sandboxed environment.
    - Given I start up my ASP.NET Core IIS application web-server, then ensure the server gives back an index page.

2. As a User, I want to have an index page, where I can see the list of Products within a default Product Category, so that I can browse Products within that Category.
    - Given I have Products and a default Product Category in the application when I open the root url (`/`) then ensure I can see a list of Products with the following details: product title, description, image, price

3. As a User, I want to have an index page, where I can filter Products by Product Categories so that I can browse Products within any Category.
    - Given I have Products and Product Categories listed on the index page when I click on a Category's title then ensure it displays the Products only in the selected Category

4. As a User, I want to have an index page, where I can filter Products by Suppliers so that I can browse Products by Suppliers too.
    - Given I have Products and Suppliers listed on the index page when I click on a Supplier's name then ensure it displays the Products only for the selected Supplier

5. As a User, I want to have a Shopping Cart so that I can add products which I want to buy.
    - Given I have a Product list and the Products have an "Add to cart" button when I click on the "Add to cart" button then ensure it creates a new Order for storing cart data of the User and ensure it creates a new LineItem with the quanity (default: 1) and price (the price of the Product) and ensure it stores this data on the server
    - Given I have a Product list and the Products have an "Add to cart" button when I click on the "Add to cart" button then ensure the number of cart items is displayed in the Page header

6. As a User, I want to review my Shopping Cart so that I can review the items in my shopping cart before checking out so that I can see what I've already selected.
    - Given I have a Shopping Cart with items in it when I click on the "Shopping cart" menu item in the Page header then ensure it displays the items (LineItems) with the following data: name of the Product, quantity, unit price / subtotal price
    - Given I have a Shopping Cart with items in it when I click on the "Shopping cart" menu item in the Page header ensure the total price of all the items in the cart is displayed

7. As a User, I want to edit the items in my Shopping Cart so that I can modify the items when I change my mind - by changing quantity or removing items.
    - Given I have a Shopping Cart review page and the LineItems are displayed in a form and the quantities are displayed in input fields when I change the quantity of an item then ensure it stores the new quantity of the LineItem
    - Given I have a Shopping Cart review page and the LineItems are displayed in a form and the quantities are displayed in input fields when I change the quantity to 0 then ensure it removes the LineItem from the cart

8. As a User, I want to checkout the items from the Shopping Cart so that I can order the Products.
    - Given I have a Shopping Cart review page when I click on the "Checkout" button then ensure it asks the following data from the User: Name, Email, Phone number, Billing Address (Country, City, Zipcode, Address), Shipping Address (Country, City, Zipcode, Address)
    - Given I have a Shopping Cart review page when I click on the "Go to Payment" button then ensure Ensure that data on the checkout form are validated
    - Given I have a Shopping Cart review page when I click on the "Go to Payment" button and data are validated successfully then ensure that data on the checkout form are stored in the Order
    - Given I have a Shopping Cart review page when I click on the "Go to Payment" button and data are validated successfully then ensure that it redirects to the Payment page

9. As a User, I want to pay for my Products so that I can complete the payment online.
    - Given I checked out the items from the Shopping cart then I can see the total price what I have to pay
    - Given I checked out the items from the Shopping cart then I can pay for my order with a credit card.
    - Given I checked out the items from the Shopping cart and chosen the payment method then based on the selected payment method I can enter the credentials for the payment provider: card number, card holder, expiry date and CVV code.

10. As a User, I want to see the result of my payment so that I can get a confirmation about my Order.
    - Given I made a payment and there was an error in the transaction then I can see the details of the error
    - Given I made a payment and the transaction was successful then I can see the Confirmation page with the details of the Order
    - Given I made a payment and the transaction was successful then then ensure it saves the Order into a `JSON` file
    - Given I made a payment and the transaction was successful then ensure it sends an email to the User about the Order

11. As an Admin, I want to have a logfile about the checkout processes (per Order) so that I can see the steps of every Order and investigate issues.
    - Given the User started a checkout process then ensure it saves all the steps and details into a JSON file (where the filename is the Order ID and Date)

## General requirements

- Advanced OOP concepts are used in the project: inheritance, there is at least on abstract class, there is at least one interface implemented
- The page doesn't show a server error anytime during the review
- All code is pushed to GitHub repository by atomic commits. The implemented feature related commits managed on separated feature branches and merged by a pull request to the `master` branch.

## Hints

- Do not use a database, now only use in-memory storage or file storage but
  through the DAO pattern (Data Access Object).
- It's not required to integrate real payment services - you can use fake
  payment implementations.

## Background materials

- <i class="far fa-exclamation"></i> [Dependency Injection](https://www.tutorialsteacher.com/ioc/dependency-injection)
- <i class="far fa-exclamation"></i> [Create a shopping cart](https://learningprogramming.net/net/asp-net-core-mvc/build-shopping-cart-with-session-in-asp-net-core-mvc/)
- <i class="far fa-exclamation"></i> [Logging with Serilog](https://www.youtube.com/watch?v=_iryZxv8Rxw)
- <i class="far fa-exclamation"></i> [Entity Framework Core In-Memory Database](https://exceptionnotfound.net/ef-core-inmemory-asp-net-core-store-database/)
- <i class="far fa-exclamation"></i> [Intro to SQL relations in the database](https://www.sqlshack.com/learn-sql-types-of-relations/)
- <i class="far fa-exclamation"></i> [JSON serialization and deserialization](https://www.c-sharpcorner.com/article/json-serialization-and-deserialization-in-c-sharp/)

