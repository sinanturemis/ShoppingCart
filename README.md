# Shopping Cart Implementation
This project is a simple implementation of a shopping cart as console application. 
Basicly, it provides these abilities by considering some predefined rules.
- Adding products onto Shopping Cart
- Applying campaigns
- Applying/removing coupon
- Calculating delivery price

## Used Tools - Language - Technologies
- C#
- .Net Core 3.1
- Visual Studio 2019 Community Edition
- dotCover
- NUnit
- Moq Library

## Test Coverage

This project developed as Test Driven. Business is covered %100. You can download the coverage report that generated by dotCover by using the link.

<a href="SolutionItems/TestCoverageReport.zip" download>Test Coverage Report</a>

## Inheritance Dependency Diagram

This is the general structure of the classes.

![Dependency Diagram](SolutionItems/DependencyDiagram.png)

### Product
This object has title, unit price and category properties. Also, contains a method **GetAllCategories** to get current and its ancestors categories.

### Category
This object has Title and ParentCategory properties. In addition, there are two methods **HasParent** and **GetParent**.

### Discount
This is an abstract class to inherit Campaign and Coupon. Calculates discount with **CalculateDiscount** method by using **DiscountStrategy**.

### Campaign
**Campaign** is a concrete class of **Discount**. It has **IsApplicable** method to understand if order quantity is suitable for the campaign.

### Coupon
This is another concrete class of **Discount**. As **Campaign** has, this class has **IsApplicable** method to understand if minimum purchase amount is suitable for the coupon.

### DiscountConcrete
Because its not possible to create an instance from abstract classes, we need to create an concrete object to test this kind of classes. This is a fake class to write unit tests for the **Discount**.

### IDiscountStrategy
This interface is a contract for the user of discount strategies (for now, they are **RateBasedDiscountStrategy** and **AmountBasedDiscountStrategy**). Thanks to this approach, a new discount strategy can be implemented without any change requirement in user class.

### DiscountBase
This class is another abstract class. It implements the **IDiscountStrategy** and basicly, it holds the discount value and give a base to **RateBasedDiscountStrategy** and **AmountBasedDiscountStrategy**.

### RateBasedDiscountStrategy
This class is a discount calculation class. Responsibility of this is calculating a rate based discount. It overrides the **CalculateDiscount** method with following formula.

> discount = (amount * DiscountValue) / 100

### AmountBasedDiscountStrategy
This is another discount calculation class. We can name it also as 'Fixed Discount'. It just returns discount amount.

### Shopping Cart Item
All products in Shopping Cart are kept in a dictionary. Key of the dictionary is productTitle and value is ShoppingCartItem. This class is basicly for each product in shopping cart. It has that properties: product, order quantity and total price for that product in cart and applied best campaign. It has **AddQuantity** and **ApplyBestCampaign** methods. 
**AddQuantity** increases the quantity of amount in cart when a existing product is added in cart. 
**ApplyBestCampaign** is run when campaigns are applied. It gets all categories (with ancestor categories) of product. Filters applicable campaigns if they are in categories that the product has and try to find the best discount amount.

### Delivery Cost Calculator
Delivery Cost Calculator has only one method **CalculateFor**. Takes shopping cart and calculates the cargo price as requested. If there is no product in cart, it will give 0 as result. Because, in this scenario, there won't be any item to deliver.

### Shopping Cart
