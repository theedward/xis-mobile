package xismobile.pim.uml2.gen.android.services;

import org.eclipse.uml2.uml.Class;
import org.eclipse.uml2.uml.Stereotype;

public final class ServiceUtils {

	// Suppress default constructor for non-instantiability
	private ServiceUtils() {
		throw new AssertionError();
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
	
	public static boolean isMenu(Class c) {
		return c.getAppliedStereotype("XIS-Mobile::XisCompositeWidget") != null;
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
}
