package fileFilter;

import org.gudy.azureus2.plugins.Plugin;
import org.gudy.azureus2.plugins.PluginConfig;
import org.gudy.azureus2.plugins.PluginException;
import org.gudy.azureus2.plugins.PluginInterface;
import org.gudy.azureus2.plugins.ui.model.BasicPluginConfigModel;



public class FileFilter implements Plugin {

	@Override
	public void initialize(PluginInterface pluginInterface)
			throws PluginException {
		
		initConfig(pluginInterface);
		
//		InputStream in;
//		Properties p = new Properties();
//		try {
//			in = new BufferedInputStream(new FileInputStream("plugins/fileFilter/keyword.properties"));
//			p.load(in);
//		} catch (FileNotFoundException e) {
//			e.printStackTrace();
//		} catch (IOException e) {
//			e.printStackTrace();
//		}
		
		DlListener dlListener=new DlListener();
		dlListener.setpConfig(pluginInterface.getPluginconfig());
		pluginInterface.getDownloadManager().addListener(dlListener);
		pluginInterface.getDownloadManager().getGlobalDownloadEventNotifier().addListener(dlListener);
	}
	
	void initConfig(PluginInterface plugin_interface){
		  BasicPluginConfigModel config_page =
				     plugin_interface.getUIManager().createBasicPluginConfigModel("FileFilter");
		  PluginConfig pConfig=plugin_interface.getPluginconfig();
		  String filterStrings= pConfig.getPluginStringParameter("filterStrings", "sample");
		  String extStrings=pConfig.getPluginStringParameter("extStrings",".htm,.html,.mht,.ext,.url,.nfo,.txt,.exe");
		  config_page.addStringParameter2("filterStrings", "filterStrings", filterStrings);
		  config_page.addStringParameter2("extStrings", "extStrings", extStrings); 
		  
		  
		  
	}



}
