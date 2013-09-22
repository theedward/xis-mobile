package xismobile.pim.uml2.gen.android.services;

import java.util.ArrayList;
import java.util.List;

import org.eclipse.uml2.uml.Association;
import org.eclipse.uml2.uml.Class;
import org.eclipse.uml2.uml.Generalization;
import org.eclipse.uml2.uml.Interface;
import org.eclipse.uml2.uml.InterfaceRealization;
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
			if (!first.isNavigable() && !second.isNavigable()
					|| first.isNavigable() && second.isNavigable()) {
				if (endTypes.get(0).getName().compareTo(c.getName()) == 0) {
					result.add(second);
				} else {
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

	public String getExtensionAndImplementations(Class c) {
		StringBuilder builder = new StringBuilder();
		List<Generalization> gens = c.getGeneralizations(); 

		if (gens.size() == 1 && isXisInheritance(gens.get(0))) {
			Class cl = (Class) gens.get(0).getTargets()
					.get(0);
			builder.append(" extends ").append(cl.getName());
		}

		if (c.getInterfaceRealizations().size() > 0) {
			if (builder.length() > 0) {
				builder.append(" ");
			}
			builder.append("implements ");
			Interface i;
			for (InterfaceRealization it : c.getInterfaceRealizations()) {
				i = (Interface) it.getTargets().get(0);
				builder.append(i.getName()).append(", ");
			}
			builder.delete(builder.length() - 2, builder.length());
		}
		return builder.toString();
	}

	public List<String> getManyToManyAssociations(Class c) {
		List<String> joinEntities = new ArrayList<String>();
		Property first = null;
		Property second = null;

		for (Association a : c.getAssociations()) {
			first = a.getMemberEnds().get(0);
			second = a.getMemberEnds().get(1);
			if (first.upperBound() == -1 && second.upperBound() == -1) {
				if (a.getEndTypes().get(0).getName().equals(c.getName())) {
					joinEntities.add(toUpperFirst(a.getEndTypes().get(0)
							.getName())
							+ toUpperFirst(a.getEndTypes().get(1).getName()));
				}
			}
		}
		return joinEntities;
	}
	
	public List<Class> getMasterEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisMasterAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}

	public List<Class> getDetailEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisDetailAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}
	
	public List<Class> getReferenceEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				first = a.getEndTypes().get(0);
				second = a.getEndTypes().get(1);
				if (isXisBusinessEntity(first)) {
					bes.add((Class) first);
				} else if (isXisBusinessEntity(second)) {
					bes.add((Class) second);
				}
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}
		return entities;
	}
	
	public List<Class> getInteractionSpaceReferencedEntities(Class c) {
		List<Class> bes = new ArrayList<Class>();
		List<Class> entities = new ArrayList<Class>();
		List<Association> assocs = new ArrayList<Association>();
		Type first = null;
		Type second = null;

		for (Association a : c.getAssociations()) {
			if (isXisDomainAssociation(a)) {
				assocs.add(a);
			}
		}

		for (Association a : assocs) {
			first = a.getEndTypes().get(0);
			second = a.getEndTypes().get(1);
			if (isXisBusinessEntity(first)) {
				bes.add((Class) first);
			} else if (isXisBusinessEntity(second)) {
				bes.add((Class) second);
			}
		}

		for (Class cl : bes) {
			for (Association a : cl.getAssociations()) {
				if (isXisMasterAssociation(a) || isXisDetailAssociation(a)
						|| isXisReferenceAssociation(a)) {
					first = a.getEndTypes().get(0);
					second = a.getEndTypes().get(1);
					if (isXisEntity(first)) {
						entities.add((Class) first);
					} else if (isXisEntity(second)) {
						entities.add((Class) second);
					}
				}
			}
		}

		return entities;
	}
	
	public List<Class> getInboundNavigationReferencedEntities(Class c) {
		List<Class> entities = new ArrayList<>();
//		Property first = null;
//		Property second = null;
//		Type endType = null;
//		
////		Element el = c.getOwner();
//		
//		for (Association a : c.getAssociations()) {
//			if (isXisNavigationAssociation(a)) {
//				first = a.getMemberEnds().get(0);
//				second = a.getMemberEnds().get(1);
//				if (first.isNavigable()) {
//					if (a.getEndTypes().get(0).getName().compareTo(c.getName()) != 0) {
////						result.add(first);
//					}
//				}
//				else {
//					if (a.getEndTypes().get(1).getName().compareTo(c.getName()) != 0) {
////						result.add(first);
//					}
//				}
//			}
//		}
		
		return entities;
	}

	public String getEntityAttributeOfWidget(String value) {
		return value.split(".")[1];
	}
	
	/**
	 * AUXILIARY METHODS REGION
	 */
	private boolean isXisEntity(Type t) {
		return t.getAppliedStereotype("XIS-Mobile::XisEntity") != null;
	}
	
	private boolean isXisDomainAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisDomainAssociation") != null;
	}
	
//	private boolean isXisNavigationAssociation(Association a) {
//		return a.getAppliedStereotype("XIS-Mobile::XisNavigationAssociation") != null;
//	}

	private boolean isXisBusinessEntity(Type t) {
		return t.getAppliedStereotype("XIS-Mobile::XisBusinessEntity") != null;
	}

	private boolean isXisMasterAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisMasterAssociation") != null;
	}

	private boolean isXisDetailAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisDetailAssociation") != null;
	}

	private boolean isXisReferenceAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisReferenceAssociation") != null;
	}
	
	private boolean isXisInheritance(Generalization g) {
		return g.getAppliedStereotype("XIS-Mobile::XisInheritance") != null;
	}
	
	public boolean stringContains(String s1, String s2) {
		return s1.contains(s2);
	}
	
	/**
	 * Auxiliary method to put the first letter of a string in upper case.
	 * 
	 * @param s The original string 
	 * 
	 * @return The string with the first letter in upper case
	 */
	private String toUpperFirst(String s) {
		return s.substring(0, 1).toUpperCase() + s.substring(1);
	}
}
