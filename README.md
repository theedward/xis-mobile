XIS-Mobile
==========

XIS-Mobile is a Master's Thesis project which intends to develop mobile applications using a Model-Driven Software Development approach with the goal of tackling the complexity of software development and the fragmentation of mobile platforms.  
	In order to accomplish this goal, it proposes a Domain Specific Language (DSL) to describe mobile applications in a platform-independent way and an Eclipse Modeling Framework-based framework which supports the language.  
The XIS-Mobile language reuses some of the best concepts used in the XIS UML profile, used for modeling interactive systems, and introduces new ones in order to be more appropriate to mobile applications design. The latest version of the XIS-Mobile language comprises six views:

- **Domain View -** Describes the domain entities relevant to the problem domain, their attributes and the relationships among them;
- **BusinessEntities View -** Describes higher-level entities that provide context to a certain use case and interaction space;
- **UseCases View -** Describes the operations a user can perform in the context of a business entity and/or an external service;
- **InteractionSpace View -** Describes each application's screen, known as interaction space, namely the UI layout, the events a certain UI component can trigger and the gestures that can be performed;
- **NavigationSpaced View -** Describes the navigation flow between the several interaction spaces with which the user interacts;
- **Architectural View -** Describes the interactions between the mobile application and other external entities (such as, internal providers, mobile apps or remote servers);

The XIS-Mobile Framework aims to:

1. Allow the specification of mobile applications using XIS-Mobile language through a visual editor;
2. Check the quality of the produced model;
3. Generate UserInteface views from the other views;
4. Generate source code from it for different platforms (Android and Windows Phone).

To accomplish these goals are being developed four modules:

1. Visual Editor based on [Sparx's Enterprise Architect (EA)](http://www.sparxsystems.com);
2. Model Validator using  [EA Validation API](http://www.sparxsystems.com/enterprise_architect_user_guide/10/automation_and_scripting/model_validation_example.html);
3. Model Generator based on [EA Model Driven Generation (MDG) Technologies](http://www.sparxsystems.com/enterprise_architect_user_guide/9.2/standard_uml_models/mdgtechnologies.html);
4. Code Generator based on [Acceleo](http://www.eclipse.org/acceleo) which allows model-to-text transformations with the guidance of code templates.

**© 2013 André Ribeiro - Instituto Superior Técnico**