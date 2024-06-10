import Button from '@mui/material/Button'
import { motion } from "framer-motion";
import { styled } from '@mui/material/styles';
import { Link } from 'react-router-dom';
import anh1 from '../../assets/home/handLeftPic1.png'
import anh2 from '../../assets/home/handLeftPic2.png'
import anh3 from '../../assets/home/logoCircle.svg'
import anh4 from '../../assets/home/aboutImage.png'
import Arrow from '../../assets/home/arrow.svg'
import Signature from '../../assets/home/Signature.png'
import BackGroundImage from '../../assets/home/backgroundImage.png'
import Rings from '../../assets/home/Rings.png'
import Earrings from '../../assets/home/Earrings.png'
import Bracelets from '../../assets/home/Bracelets.png'
import backgroundSection from '../../assets/home/backgroundSection.png'
import ArrowWhite from '../../assets/home/arrowWhite.svg'
import LogoCircleWhite from '../../assets/home/logoCircleWhite.svg'
import Design1 from '../../assets/home/design1.png'
import Design2 from '../../assets/home/design2.png'
import Design3 from '../../assets/home/design3.png'
import Brand1 from '../../assets/home/brand1.svg'
import Brand2 from '../../assets/home/brand2.svg'
import Brand3 from '../../assets/home/brand3.svg'
import Brand4 from '../../assets/home/brand4.svg'
import Brand5 from '../../assets/home/brand5.svg'
import Brand6 from '../../assets/home/brand6.svg'
import Blog1 from '../../assets/home/blog1.png'
import Blog2 from '../../assets/home/blog2.png'
import Blog3 from '../../assets/home/blog3.png'




export const CustomButton = styled(Button)({
      '&:hover': {
            backgroundColor: '#fff',
            color: '#000',
            border: '1px solid #000'
      }
})

function Home() {
      return (
            <>
                  <div className='px-[1.5rem] pt-[5rem]'>
                        <div className='max-w-[75rem] ml-auto mr-auto pt-[8.75rem] pb-[4.375rem] flex justify-between'>
                              <motion.div initial={{ opacity: 0, x: -50 }}
                                    animate={{ opacity: [0, 0.3, 0.5, 0.7, 1], x: 0 }}
                                    className='max-w-[39.3rem]'>
                                    <h1 className='text-[#000] my-0 text-[4rem] font-normal leading-[4.5rem]'>
                                          Choose jewelry with heart & feelings
                                          <span style={{ backgroundImage: `url(${BackGroundImage})`, backgroundPosition: '50%', backgroundRepeat: 'no-repeat', backgroundSize: 'cover' }} className='w-[8rem] inline-block ml-[0.7rem]' > &nbsp;&nbsp; </span>
                                    </h1>
                                    <div className='w-[100%] pb-[1.875rem]'></div>
                                    <p className='text-[#000] mb-0 text-[1rem] font-normal leading-[1.5rem]'>
                                          Welcome to a realm of timeless beauty and unparalleled craftsmanship. At Diamond&FPT, we invite you to adorn yourself in the finest expressions of luxury. Our curated selection showcases meticulously designed pieces that seamlessly blend tradition with contemporary allure.
                                          <br></br>
                                          <br></br>
                                          Embrace the allure of our jewelry and make a statement that echoes sophistication, individuality, and everlasting elegance. Unveil the extraordinary with Diamond&FPT
                                    </p>
                                    <div className='pb-[2.5rem] w-[100%]'></div>
                                    <Link to='/design'>
                                          <CustomButton variant='contained' sx={{ color: '#fff', bgcolor: '#000', letterSpacing: 4, padding: '0.7rem 2.375rem', fontSiz: '1rem', fontWeight: 400, lineHeight: '1.5rem', }} >
                                                EXPLORE COLLECTIONS
                                          </CustomButton>
                                    </Link>
                              </motion.div>
                              <div className='flex items-end relative gap-x-[1.875rem]'>
                                    <img className='inline-block max-w-[100%] align-middle' src={anh1} />
                                    <img className='inline-block max-w-[100%] align-middle' src={anh2} />
                                    <img className='inline-block max-w-[100%] align-middle absolute top-[46px] right-[54px]' src={anh3} />
                              </div>
                        </div>
                  </div>

                  {/* Phan about */}
                  <div className='px-[1.5rem]'>
                        <div className='max-w-[75rem] ml-auto mr-auto w-[100%] pb-[11.56rem] pt-[4.375rem]'>
                              <div className='flex items-center justify-start relative'>
                                    <div>
                                          <img className='w-[1200px] flex-1' src={anh4} />
                                    </div>
                                    <div className='flex absolute flex-col items-start max-w-[35.2rem] pt-[5.25rem] pr-[3.75rem] pb-[6rem] pl-[5.56rem] right-[0] bottom-[-115px] bg-[white] border-[2px] divide-solid border-[#eee]'>
                                          <h3 className='text-[2.375rem] font-normal leading-[2.875rem]'>Diamond&FPT Story</h3>
                                          <div className='w-[100%] pb-[1.25rem]'></div>
                                          <p className='text-[1rem] font-normal leading-[1.5rem]'>Welcome to Diamond&FPT - where creativity and the essence of jewelry come together. We take pride in offering unique jewelry designs that blend the craftsmanship of artisans with the natural beauty of precious stones. Each piece at Diamond&FPT is not just a piece of jewelry, but a work of art filled with dedication and passion. Let us help you celebrate your personal style and cherish memorable moments with our exquisite jewelry.</p>
                                          <div className='w-[100%] pb-[1.875rem]'></div>
                                          <a className='tracking-[4px] flex font-normal items-center justify-start text-[1rem] leading-[1.5rem]'>
                                                <div>LEARN MORE</div>
                                                <img className='inline-block max-w-[100%] align-middle translate-x-[-1.4rem]' src={Arrow} />
                                          </a>
                                          <img className='absolute right-[40px] bottom-[40px]' src={Signature} />
                                    </div>
                              </div>
                        </div>
                  </div>

                  {/* Phan phan loai collection */}
                  <div className='px-[1.5rem]'>
                        <div className='max-w-[75rem] ml-auto mr-auto w-[100%] pt-[4.375rem] pb-[8.75rem] '>
                              <div>
                                    <div className='flex items-center justify-center flex-col'>
                                          <h2 className='text-[2.625rem] font-normal leading-[3.5rem]'>Explore our Collections</h2>
                                    </div>
                                    <div className='pb-[3.75rem] w-[100%]'></div>
                                    <div className='w-[100%]'>
                                          <div className='w-[100%] grid gap-x-[1.5rem] gap-y-[1.5rem] grid-rows-1 grid-cols-4'>
                                                <div>
                                                      <Link to='/design/earring' style={{ filter: 'grayscale(100%)' }} className='flex relative items-center flex-col cursor-none max-w-[100%]'>
                                                            <img src={Earrings} className='inline-block max-w-[100%] align-middle' />
                                                            <div style={{ backgroundColor: 'rgb(255,255,255,0.5)' }} className='border-[1px] divide-solid border-[white] py-[0.625rem] px-[1.5rem] absolute bottom-[218px]'>
                                                                  <h6 className='text-[1.25rem] font-normal leading-[1.75rem]'>Earrings</h6>
                                                            </div>
                                                      </Link>
                                                </div>

                                                <div>
                                                      <Link to='/design/bracelet' style={{ filter: 'grayscale(100%)' }} className='flex relative items-center flex-col cursor-none max-w-[100%]'>
                                                            <img src={Bracelets} className='inline-block max-w-[100%] align-middle' />
                                                            <div style={{ backgroundColor: 'rgb(255,255,255,0.5)' }} className='border-[1px] divide-solid border-[white] py-[0.625rem] px-[1.5rem] absolute bottom-[218px]'>
                                                                  <h6 className='text-[1.25rem] font-normal leading-[1.75rem]'>Bracelets</h6>
                                                            </div>
                                                      </Link>
                                                </div>

                                                <div>
                                                      <Link to='/design/necklace' style={{ filter: 'grayscale(100%)' }} className='flex relative items-center flex-col cursor-none max-w-[100%]'>
                                                            <img src={Rings} className='inline-block max-w-[100%] align-middle' />
                                                            <div style={{ backgroundColor: 'rgb(255,255,255,0.5)' }} className='border-[1px] divide-solid border-[white] py-[0.625rem] px-[1.5rem] absolute bottom-[218px]'>
                                                                  <h6 className='text-[1.25rem] font-normal leading-[1.75rem]'>Necklaces</h6>
                                                            </div>
                                                      </Link>
                                                </div>

                                                <div>
                                                      <Link to='/design/ring' style={{ filter: 'grayscale(100%)' }} className='flex relative items-center flex-col cursor-none max-w-[100%]'>
                                                            <img src={Rings} className='inline-block max-w-[100%] align-middle' />
                                                            <div style={{ backgroundColor: 'rgb(255,255,255,0.5)' }} className='border-[1px] divide-solid border-[white] py-[0.625rem] px-[1.5rem] absolute bottom-[218px]'>
                                                                  <h6 className='text-[1.25rem] font-normal leading-[1.75rem]'>Rings</h6>
                                                            </div>
                                                      </Link>
                                                </div>

                                          </div>
                                    </div>
                              </div>
                        </div>
                  </div>

                  {/* Phan background */}
                  <div style={{ backgroundImage: `url(${backgroundSection})`, backgroundPosition: '50%', backgroundRepeat: 'no-repeat', backgroundSize: 'cover' }}>
                        <div className='px-[1.5rem] max-w-[75rem] ml-auto mr-auto w-[100%]'>
                              <div className='py-[8.75rem]'>
                                    <div className='max-w-[53.5rem] relative'>
                                          <div className='max-w-[39rem]'>
                                                <h2 className='text-[white] text-[2.625rem] font-normal leading-[3.5rem]'>Crafting diamond-shaped love stories time after time.</h2>
                                                <div className='pb-[1.25rem] w-[100%]'></div>
                                                <p className='text-[#cbcbcb] text-[1rem] leading-[1.5rem] font-normal'>
                                                      Indulge in the opulence of handcrafted necklaces, bracelets, earrings, and rings, each a masterpiece in its own right. Immerse yourself in the sparkle of meticulously selected gemstones and precious metals, expertly fashioned to create pieces that transcend trends and stand the test of time.
                                                </p>
                                          </div>
                                          <div className='pb-[2.5rem] w-[100%]'></div>
                                          <a className='text-[white] tracking-[4px] flex font-normal items-center justify-start '>
                                                <div>SHOP NEW COLLECTIONS</div>
                                                <img className='inline-block max-w-[100%] align-middle translate-x-[-1.4rem]' src={ArrowWhite} />
                                          </a>
                                          <img className='inline-block max-w-[100%] align-middlec absolute right-[0] top-[95px]' src={LogoCircleWhite} />
                                    </div>
                              </div>
                        </div>
                  </div>

                  {/* Phan new design */}
                  <div className='px-[1.5rem]'>
                        <div className='max-w-[75rem] ml-auto mr-auto w-[100%] pt-[8.75rem] pb-[4.375rem]'>
                              <div>
                                    <div className='flex items-center justify-between'>
                                          <div className='max-w-[42.2rem]'>
                                                <h2 className='text-[2.625rem] font-normal leading-[3.5rem]'>Our new design of jewelry</h2>
                                          </div>
                                    </div>
                                    <div className='w-[100%] pb-[3.75rem]'></div>
                                    <div>
                                          <div className='grid gap-x-[1.5rem] grid-rows-1 grid-cols-3'>
                                                <div>
                                                      <a className='relative overflow-hidden max-w-[100%] inline-block '>
                                                            <img className='inline-block max-w-[100%] overflow-hidden' src={Design1} />
                                                            <div className='absolute top-[24px] left-[30px] font-light tracking-[1px] text-[1rem] leading-[1.5rem]'>Description of the design</div>
                                                      </a>
                                                </div>
                                                <div>
                                                      <a className='relative overflow-hidden max-w-[100%] inline-block '>
                                                            <img className='inline-block max-w-[100%] overflow-hidden' src={Design2} />
                                                            <div className='absolute top-[24px] left-[30px] font-light tracking-[1px] text-[1rem] leading-[1.5rem]'>Description of the design</div>
                                                      </a>
                                                </div>
                                                <div>
                                                      <a className='relative overflow-hidden max-w-[100%] inline-block '>
                                                            <img className='inline-block max-w-[100%] overflow-hidden' src={Design3} />
                                                            <div className='absolute top-[24px] left-[30px] font-light tracking-[1px] text-[1rem] leading-[1.5rem]'>Description of the design</div>
                                                      </a>
                                                </div>
                                          </div>
                                    </div>
                              </div>
                        </div>
                  </div>

                  {/* Phan brand */}
                  <div className='px-[1.5rem]'>
                        <div className='max-w-[75rem] ml-auto mr-auto w-[100%] pt-[4.375rem] pb-[8.75rem]'>
                              <div className='flex overflow-hidden py-[1.875rem] items-center border-y-[2px] divide-solid border-[#eee]'>
                                    <div className='flex items-center justify-start flex-none gap-x-[5.125rem]'>
                                          <img src={Brand1} />
                                          <img src={Brand2} />
                                          <img src={Brand3} />
                                          <img src={Brand4} />
                                          <img src={Brand5} />
                                          <img src={Brand6} />
                                    </div>
                              </div>
                        </div>
                  </div>

                  {/* Phan Blog */}
                  <div className='px-[1.5rem]'>
                        <div className='max-w-[75rem] ml-auto mr-auto w-[100%] pt-[4.75rem] pb-[8.375rem]'>
                              <div>
                                    <div className='flex items-center justify-between'>
                                          <div className='max-w-[27.25rem] '>
                                                <h2 className='text-[2.625rem] font-normal leading-[3.5rem]'>Stay informed on our latest blog</h2>
                                          </div>
                                          <a className='tracking-[4px] flex items-center justify-start font-normal '>
                                                <div>VIEW MORE</div>
                                                <img className='translate-x-[-1.4rem]' src={Arrow} />
                                          </a>
                                    </div>
                                    <div className='w-[100%] pb-[3.75rem]'></div>

                                    <div>
                                          <div className='grid gap-x-[1.5rem] grid-rows-1 grid-cols-3'>
                                                <div>
                                                      <div>
                                                            <img src={Blog1} />
                                                            <div className='w-[100%] pb-[1.25rem]'></div>
                                                            <div>
                                                                  <h6 className='text-[1.25rem] font-normal leading-[1.75rem]'>Title of article 1</h6>
                                                                  <div className='w-[100%] pb-[1.875rem]'></div>
                                                                  <a className='flex items-start flex-col gap-y-[0.6rem] overflow-hidden max-w-[21%]'>
                                                                        <div className='text-[1rem] font-normal leading-[1.5rem]'>Read more</div>
                                                                        <div className='w-[100%] h-[1px] bg-[#000]'></div>
                                                                  </a>
                                                            </div>
                                                      </div>
                                                </div>

                                                <div>
                                                      <div>
                                                            <img src={Blog2} />
                                                            <div className='w-[100%] pb-[1.25rem]'></div>
                                                            <div>
                                                                  <h6 className='text-[1.25rem] font-normal leading-[1.75rem]'>Title of article 2</h6>
                                                                  <div className='w-[100%] pb-[1.875rem]'></div>
                                                                  <a className='flex items-start flex-col gap-y-[0.6rem] overflow-hidden max-w-[21%]'>
                                                                        <div className='text-[1rem] font-normal leading-[1.5rem]'>Read more</div>
                                                                        <div className='w-[100%] h-[1px] bg-[#000]'></div>
                                                                  </a>
                                                            </div>
                                                      </div>
                                                </div>

                                                <div>
                                                      <div>
                                                            <img src={Blog3} />
                                                            <div className='w-[100%] pb-[1.25rem]'></div>
                                                            <div>
                                                                  <h6 className='text-[1.25rem] font-normal leading-[1.75rem]'>Title of article 3</h6>
                                                                  <div className='w-[100%] pb-[1.875rem]'></div>
                                                                  <a className='flex items-start flex-col gap-y-[0.6rem] overflow-hidden max-w-[21%]'>
                                                                        <div className='text-[1rem] font-normal leading-[1.5rem]'>Read more</div>
                                                                        <div className='w-[100%] h-[1px] bg-[#000]'></div>
                                                                  </a>
                                                            </div>
                                                      </div>
                                                </div>
                                          </div>
                                    </div>
                              </div>
                        </div>
                  </div>
            </>
      )
}

export default Home