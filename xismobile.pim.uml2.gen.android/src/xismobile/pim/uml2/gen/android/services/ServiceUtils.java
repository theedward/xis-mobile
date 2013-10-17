package xismobile.pim.uml2.gen.android.services;

import org.eclipse.uml2.uml.Association;
import org.eclipse.uml2.uml.Class;
import org.eclipse.uml2.uml.Element;
import org.eclipse.uml2.uml.EnumerationLiteral;
import org.eclipse.uml2.uml.Generalization;
import org.eclipse.uml2.uml.Stereotype;
import org.eclipse.uml2.uml.Type;

public final class ServiceUtils {

	// Suppress default constructor for non-instantiability
	private ServiceUtils() {
		throw new AssertionError();
	}
	
	public static Stereotype getXisInteractionSpace(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisInteractionSpace");
	}
	
	public static boolean xisInteractionSpaceHasTitle(Class c) {
		Stereotype space = c.getAppliedStereotype("XIS-Mobile::XisInteractionSpace");
		if (space != null) {
			String title = (String) c.getValue(space, "title");
			return title != null && !title.isEmpty();
		} else {
			return false;
		}
	}
	
	public static Class geXisInteractionSpacetMenu(Class c) {
		Class menu = null;
		for (Element el : c.getOwnedElements()) {
			if (el instanceof Class) {
				Class aux = (Class) el;
				if (getMenu(aux) != null) {
					menu = aux;
					break;
				}
			}
		}
		return menu;
	}
	
	public static boolean isXisLabel(Class c) {
		return getXisLabel(c) != null;
	}
	
	public static Stereotype getXisLabel(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisLabel");
	}
	
	public static boolean isXisTextBox(Class c) {
		return getXisTextBox(c) != null;
	}

	public static Stereotype getXisTextBox(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisTextBox");
	}
	
	public static boolean isXisCheckBox(Class c) {
		return getXisCheckBox(c) != null;
	}
	
	public static Stereotype getXisCheckBox(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCheckBox");
	}
	
	public static boolean isXisImage(Class c) {
		return getXisImage(c) != null;
	}
	
	public static Stereotype getXisImage(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisImage");
	}
	
	public static boolean isXisButton(Class c) {
		return getXisButton(c) != null;
	}
	
	public static Stereotype getXisButton(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisButton");
	}
	
	public static boolean isXisLink(Class c) {
		return getXisLink(c) != null;
	}
	
	public static Stereotype getXisLink(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisLink");
	}
	
	public static boolean isXisDatePicker(Class c) {
		return getXisDatePicker(c) != null;
	}
	
	public static Stereotype getXisDatePicker(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisDatePicker");
	}
	
	public static boolean isXisTimePicker(Class c) {
		return getXisTimePicker(c) != null;
	}
	
	public static Stereotype getXisTimePicker(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisTimePicker");
	}
	
	public static boolean isXisWebView(Class c) {
		return getXisWebView(c) != null;
	}
	
	public static Stereotype getXisWebView(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisWebView");
	}
	
	public static boolean isXisMapView(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisMapView") != null;
	}
	
	public static Stereotype getXisMapView(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisMapView");
	}
	
	public static boolean isXisDropdown(Class c) {
		return getXisDropdown(c) != null;
	}
	
	public static Stereotype getXisDropdown(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisDropdown");
	}
	
	public static boolean isXisCompositeWidget(Class c) {
		return getXisCompositeWidget(c) != null;
	}
	
	public static Stereotype getXisCompositeWidget(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget");
	}
	
	public static Stereotype getMenu(Class c) {
		Stereotype menu = c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget");
		if (menu != null) {
			EnumerationLiteral type = (EnumerationLiteral) c.getValue(menu, "type");
			if (type.getName().equals("Menu")) {
				return menu;
			} else {
				return null; 
			}
		} else {
			return null;
		}
	}
	
	public static boolean isContextMenu(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget") != null;
	}
	
	public static boolean isList(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget") != null;
	}
	
	public static boolean isItem(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget") != null;
	}
	
	public static boolean isForm(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget") != null;
	}
	
	public static boolean isTab(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget") != null;
	}
	
	public static Stereotype getWidgetStereotype(Class c) {
		Stereotype s = null;
		
		if (isXisLabel(c)) {
			s = getXisLabel(c);
		} else if (isXisTextBox(c)) {
			s = getXisTextBox(c);
		} else if (isXisCheckBox(c)) {
			s = getXisCheckBox(c);
		} else if (isXisLink(c)) {
			s = getXisLink(c);
		} else if (isXisButton(c)) {
			s = getXisButton(c);
		} else if (isXisImage(c)) {
			s = getXisImage(c);
		} else if (isXisDatePicker(c)) {
			s = getXisDatePicker(c);
		} else if (isXisTimePicker(c)) {
			s = getXisTimePicker(c);
		} else if (isXisWebView(c)) {
			s = getXisWebView(c);
		} else if (isXisMapView(c)) {
			s = getXisMapView(c);
		} else if (isXisDropdown(c)) {
			s = getXisDropdown(c);
		} else if (isXisCompositeWidget(c)) {
			s = getXisCompositeWidget(c);
		}
		return s;
	}
	
	public static int getPosX(Class c, Stereotype s) {
		int posX = (int) c.getValue(s, "posX"); 
		return posX;
	}
	
	public static int getPosY(Class c, Stereotype s) {
		int posY = (int) c.getValue(s, "posY");
		return posY;
	}
	
	public static int getWidth(Class c, Stereotype s) {
		int width = (int) c.getValue(s, "width");
		return width;
	}
	
	public static int getHeight(Class c, Stereotype s) {
		int height = (int) c.getValue(s, "height");
		return height;
	}
	
	public static String getWidgetName(Class c) {
		String name = null;
		if (isXisLabel(c)) {
			name = "label";
		} else if (isXisTextBox(c)) {
			name = "editText";
		} else if (isXisCheckBox(c)) {
			name = "checkBox";
		} else if (isXisLink(c)) {
			name = "link";
		} else if (isXisButton(c)) {
			name = "button";
		} else if (isXisImage(c)) {
			name = "image";
		} else if (isXisDatePicker(c)) {
			name = "datePicker";
		} else if (isXisTimePicker(c)) {
			name = "timePicker";
		} else if (isXisWebView(c)) {
			name = "webView";
		} else if (isXisMapView(c)) {
			name = "mapView";
		} else if (isXisDropdown(c)) {
			name = "spinner";
		} else if (isXisCompositeWidget(c)) {
			
		}
		if (name != null && name.length() > 0) {
			name += toUpperFirst(c.getName());
		}
		return name;
	}
	
	public static boolean isXisEntity(Type t) {
		return t.getAppliedStereotype("XIS-Mobile::XisEntity") != null;
	}
	
	public static boolean isXisDomainAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisDomainAssociation") != null;
	}
	
//	public static boolean isXisNavigationAssociation(Association a) {
//		return a.getAppliedStereotype("XIS-Mobile::XisNavigationAssociation") != null;
//	}

	public static boolean isXisBusinessEntity(Type t) {
		return t.getAppliedStereotype("XIS-Mobile::XisBusinessEntity") != null;
	}

	public static boolean isXisMasterAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisMasterAssociation") != null;
	}

	public static boolean isXisDetailAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisDetailAssociation") != null;
	}

	public static boolean isXisReferenceAssociation(Association a) {
		return a.getAppliedStereotype("XIS-Mobile::XisReferenceAssociation") != null;
	}
	
	public static boolean isXisInheritance(Generalization g) {
		return g.getAppliedStereotype("XIS-Mobile::XisInheritance") != null;
	}
	
	/**
	 * Puts the first letter of a string in upper case and returns it.
	 * 
	 * @param s The original string 
	 * @return The string with the first letter in upper case
	 */
	private static String toUpperFirst(String s) {
		return s.substring(0, 1).toUpperCase() + s.substring(1);
	}
}
