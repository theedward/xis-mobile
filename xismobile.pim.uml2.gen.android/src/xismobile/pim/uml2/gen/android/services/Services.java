package xismobile.pim.uml2.gen.android.services;

import java.util.ArrayList;
import java.util.List;

import org.eclipse.uml2.uml.Association;
import org.eclipse.uml2.uml.Class;
import org.eclipse.uml2.uml.Property;
import org.eclipse.uml2.uml.Type;


public class Services {

	public List<String> getReferencedEntities(Class c) {
		List<String> entities = new ArrayList<String>();

		for (Association a : c.getAssociations()) {
			entities.add(a.getEndTypes().get(1).getName());
		}
		return entities;
	}
	
	public List<Property> getClassAssociationAttributes(Class c) {
		List<Type> endTypes = null;
		Property first = null;
		Property second = null;
		List<Property> memberEnds = null;
		List<Property> result = new ArrayList<Property>();
		
		for (Association a : c.getAssociations()) {
			endTypes = a.getEndTypes();
			memberEnds = a.getMemberEnds();
			first = memberEnds.get(0);
			second = memberEnds.get(1);
			if (!first.isNavigable() && !second.isNavigable() ||
				first.isNavigable() && second.isNavigable()) {
				if (endTypes.get(0).getName().compareTo(c.getName()) == 0) {
					result.add(second);
				}
				else {
					result.add(first);
				}
			} else if (first.isNavigable()) {
				if (endTypes.get(0).getName().compareTo(c.getName()) != 0) {
					result.add(first);
				}
			} else if (second.isNavigable()) {
				if (endTypes.get(1).getName().compareTo(c.getName()) != 0) {
					result.add(second);
				}
			}
		}
		return result;
	}

}
