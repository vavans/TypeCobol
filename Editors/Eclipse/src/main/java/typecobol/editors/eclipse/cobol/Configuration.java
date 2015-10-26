package typecobol.editors.eclipse.cobol;

import org.eclipse.jface.text.IDocument;
import org.eclipse.jface.text.presentation.IPresentationReconciler;
import org.eclipse.jface.text.presentation.PresentationReconciler;
import org.eclipse.jface.text.rules.DefaultDamagerRepairer;
import org.eclipse.jface.text.source.ISourceViewer;
import org.eclipse.jface.text.source.SourceViewerConfiguration;

import typecobol.editors.eclipse.ColorMap;
import typecobol.editors.eclipse.MarkerHandler;

public class Configuration extends SourceViewerConfiguration {
	private Scanner scanner;

	public Configuration(final MarkerHandler handler, final ColorMap colors) {
		this.scanner = new Scanner(handler, colors);
	}
	@Override
	public String[] getConfiguredContentTypes(ISourceViewer sourceViewer) {
		return new String[] { IDocument.DEFAULT_CONTENT_TYPE };
	}

	@Override
	public IPresentationReconciler getPresentationReconciler(ISourceViewer sourceViewer) {
		DefaultDamagerRepairer dr = new DefaultDamagerRepairer(scanner);
		PresentationReconciler reconciler = new PresentationReconciler();
		reconciler.setDamager(dr, IDocument.DEFAULT_CONTENT_TYPE);
		reconciler.setRepairer(dr, IDocument.DEFAULT_CONTENT_TYPE);
		return reconciler;
	}

}
