XIS-Mobile
==========

XIS-Mobile is a Master's Thesis project which intends to develop mobile applications using a Model-Driven Software Development approach with the goal of tackling the complexity of software development and the fragmentation of mobile platforms.  
	In order to accomplish this goal, it proposes a Domain Specific Language (DSL) to describe mobile applications in a platform-independent way and an Eclipse Modeling Framework-based framework which supports the language.  
The XIS-Mobile language reuses some of the best concepts used in the XIS UML profile, used for modeling interactive systems, and introduces new ones in order to be more appropriate to mobile applications design. The first version of the XIS-Mobile language comprises three views:

- **Domain View -** Describes the domain entities and the relationships among them;
- **InteractionSpace View -** Describes each application's screen, known as interaction space, namely the UI layout, the events a certain UI component can trigger and the gestures that can be performed;
- **NavigationSpaced View -** Describes the navigation flow between the several interaction spaces with which the user interacts.

The XIS-Mobile Framework XIS-Mobile Framework aims to:

1. Allow the specification of mobile applications using XIS-Mobile language through a visual editor;
2. Check the quality of the produced model;
3. Generate source code from it for Android and Windows Phone applications.

To accomplish these goals are being developped three modules:

1. Visual Editor based on [Papyrus](http://www.eclipse.org/papyrus/). In the future release, [Sparx Enterprise Architect](http://www.sparxsystems.com/) will be used as the Visual Editor;
2. Model Validator using the [Epsilon Validation Language (EVL)](http://www.eclipse.org/epsilon/doc/evl/);
3. Code Generator based on [Acceleo](http://www.eclipse.org/acceleo/) which allows model-to-text transformations with the guidance of code templates.