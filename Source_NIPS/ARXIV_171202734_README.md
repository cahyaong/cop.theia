# ChemNet: A Transferable and Generalizable Deep Neural Network for Small-Molecule Property Prediction

> With access to large datasets, deep neural networks (DNN) have achieved humanlevel accuracy in image and speech recognition tasks. However, in chemistry, availability of large standardized and labelled datasets is scarce, and many chemical properties of research interest, chemical data is inherently small and fragmented. In this work, we explore transfer learning techniques in conjunction with the existing Chemception CNN model, to create a transferable and generalizable deep neural network for small-molecule property prediction. Our latest model, ChemNet learns in a semi-supervised manner from inexpensive labels computed from the ChEMBL database. When fine-tuned to the Tox21, HIV and FreeSolv dataset, which are 3 separate chemical properties that ChemNet was not originally trained on, we demonstrate that ChemNet exceeds the performance of existing Chemception models and other contemporary DNN models. Furthermore, as ChemNet has been pre-trained on a large diverse chemical database, it can be used as a general-purpose plug-and-play deep neural network for the prediction of novel small-molecule chemical properties.

## Setup Instruction

#### Data Source

#### Dependencies

#### Quick Start

## Process

#### Data Preparation

1. Generate molecular descriptors from a given SMILES string using *RDKit* library;
  *[[Jupyter]](./ARXIV_171202734_00_Development.ipynb)*

2. Preprocess all compounds on the ChEMBL database to generate the input and output datasets for the training process;
  *[[Jupyter]](./ARXIV_171202734_01_DataPreparation.ipynb)*

3. ...

#### Data Splitting

#### Neural Network Training

## Conclusion