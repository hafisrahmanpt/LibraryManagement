Library Management System:
____________________________


The idea within here is Librarian is the admin who can only be able to register new members and add new books, at the time of 
registering a new member. Details like Name, Address, Phone, Active Status, Email, password are the data registering for a member
by the Admin/Librabrian.this Email and Password are used to login a member to see member portal. 
Admin can also view all the members,edit memebr data, activity status other than emil and password.
Books cal also be added,edited adn viewed. Can view the most available genre, most checked out books and total number of active 
members in the dashboard. 

A member can login to their dashboard using their credentials given, and can see their activity status if they are active or not.
and a personalised message based on activity status.
(can add a change password Functionality Later)


Steps to run the project:
____________________________

* Open the file in Visual Studio.

*Start the Application.you will be redirect to the member login page. just below can see the label "Librarian Login".click to 

redirect to the Librarian login portal. Use   UserName: Librarian321
                                              Password: 123456789



Challenges Faced:
____________________

During Develoment i have faced many challenges. the logic for cretaing member along with their login credentials to login them back
was challenging.  as it is not used the normal registration-login components. here it is just adding a member into database and fetching
data while signing in by fetching Email and corresponing password and also the member correspondant status. 
Just like that the Librarian registration or login was challlenging. as the librarian has a single time registering. so i 
implemenrted the HardCoded credentiasl for libarian registration. rather than creating a regn. page.


the Navbar with in the Logout has been hided based in the logic should only be seen to librarian. but the condition wont work
as expected. it can be made possible by reading the proper Authentication while librarian login and the condition should be 
satisfied. and i havent provided the security for user registration and data storage. it can be solved by using hashing techniques,
while saving into the database 
