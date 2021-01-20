import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;
import javax.imageio.ImageIO;
import org.jcodec.api.SequenceEncoder;
import org.jcodec.api.awt.AWTSequenceEncoder;
import org.jcodec.common.io.NIOUtils;
import org.jcodec.common.io.SeekableByteChannel;
import org.jcodec.common.*;

import com.sun.pdfview.PDFFile;
import com.sun.pdfview.PDFPage;




public class main {


    public static void main(String[] arg) throws IOException {
        String[]args =new String[2];
        args[0]="C:\\Users\\1810524\\Documents\\NetBeansProjects\\JavaApplication1\\src\\javaapplication1\\1\\2.PDF";
        args[1]="C:\\Users\\1810524\\Documents\\NetBeansProjects\\JavaApplication1\\src\\javaapplication1\\1\\";
        if(args.length!=2)
        {
            System.err.println("Usage:Pdf2Image pdf imageFolder");
            return;
        }
        File file = new File(args[0]);
        RandomAccessFile raf;
        try {
            raf = new RandomAccessFile(file, "r");

            FileChannel channel = raf.getChannel();
            ByteBuffer buf = channel.map(FileChannel.MapMode.READ_ONLY, 0, channel.size());
            PDFFile pdffile = new PDFFile(buf);
            // draw the first page to an image
            int num=pdffile.getNumPages();
            for(int i=1;i<=num;i++)
            {
                PDFPage page = pdffile.getPage(i);

                //get the width and height for the doc at the default zoom
                int width=(int)page.getBBox().getWidth();
                int height=(int)page.getBBox().getHeight();
                Rectangle rect = new Rectangle(0,0,width,height);
                int rotation=page.getRotation();
                Rectangle rect1=rect;
                if(rotation==90 || rotation==270)
                    rect1=new Rectangle(0,0,rect.height,rect.width);

                //generate the image
                BufferedImage img = (BufferedImage)page.getImage(
                        rect.width, rect.height, //width & height
                        rect1, // clip rect
                        null, // null for the ImageObserver
                        true, // fill background with white
                        true  // block until drawing is done
                );

               
                ImageIO.write(img, "png", new File(args[1]+i+".png"));
                BufferedImage imgs = ImageIO.read(new File(args[1]+i+".png"));
                Graphics g = imgs.getGraphics();
                g.setColor(Color.black);
                g.drawString("stepanov",1 , 100);
                g.dispose();
                ImageIO.write(imgs, "png", new File(args[1]+i+".png"));
            }
        }
        catch (FileNotFoundException e1) {
            System.err.println(e1.getLocalizedMessage());
        } catch (IOException e) {
            System.err.println(e.getLocalizedMessage());
        }
        File s= new File("C:\\Users\\1810524\\Documents\\NetBeansProjects\\JavaApplication1\\src\\javaapplication1\\1\\test.mp4");
        AWTSequenceEncoder enc = AWTSequenceEncoder.createSequenceEncoder(s,25);
        int framestoEncode = 100;
        int len = new File("C:\\Users\\1810524\\Documents\\NetBeansProjects\\JavaApplication1\\src\\javaapplication1\\1\\").list().length-2;
        for(int j=1;j<=len;++j){
        for(int i=1;i<=framestoEncode/2;++i)
        {
            BufferedImage image = ImageIO.read(new File("C:\\Users\\1810524\\Documents\\NetBeansProjects\\JavaApplication1\\src\\javaapplication1\\1\\"+j+".png"));
            enc.encodeImage(image);
        }
        }


        enc.finish();
    }

}